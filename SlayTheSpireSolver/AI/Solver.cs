using System.Collections.Concurrent;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.AI;

public class Solver
{
    private readonly ConcurrentDictionary<GameState, ExpectedValue> _gameStateCache = new();
    public int EvaluatedActions;
    public int EvaluatedGameStates;
    public int GameStateCacheHits;
    public int PrunedActionOutcomes;
    public int GameStateSearchDepth { get; init; } = 3;

    // TODO:
    //  * Parallelize
    //  * Improve non-terminal game state estimation
    //  * Improve Rules Engine performance

    public (PlayerAction, ExpectedValue) FindBestAction(GameState gameState)
    {
        if (!gameState.GetLegalActions().Any()) throw new ArgumentException("Game state has no legal actions");

        var gameStateDepthLimit = GameStateSearchDepth - 1;
        var actions = gameState.GetLegalActions().OrderByDescending(GetActionPriority).ToList();
        var cutoffAction = actions.First();
        var cutoffExpectedValue = FindExpectedValue(cutoffAction, gameStateDepthLimit);
        var cutoffValue = cutoffExpectedValue.Range.Minimum;
        return actions
            .Select(action => (action, FindExpectedValue(action, gameStateDepthLimit, cutoffValue)))
            .Append((cutoffAction, cutoffExpectedValue))
            .MaxBy(tuple => tuple.Item2);
    }

    private ExpectedValue FindExpectedValue(GameState gameState, int gameStateDepthLimit)
    {
        var isCached = _gameStateCache.TryGetValue(gameState, out var cachedResult);
        if (isCached)
        {
            Interlocked.Increment(ref GameStateCacheHits);
            return cachedResult!;
        }

        Interlocked.Increment(ref EvaluatedGameStates);
        var result = FindExpectedValueUncached(gameState, gameStateDepthLimit);
        _gameStateCache.TryAdd(gameState, result);
        return result;
    }

    private ExpectedValue FindExpectedValueUncached(GameState gameState, int gameStateDepthLimit)
    {
        var playerHealth = Math.Max(gameState.PlayerHealth.Amount, 0);
        if (gameState.IsCombatOver()) return new ExpectedValue(playerHealth);
        if (gameStateDepthLimit <= 0) return new ExpectedValue(new Range(0, playerHealth));

        gameStateDepthLimit -= 1;
        var playerActions = gameState.GetLegalActions().OrderByDescending(GetActionPriority).ToList();
        var bestEstimate = double.NegativeInfinity;
        var bestMinimum = double.NegativeInfinity;
        var bestMaximum = double.NegativeInfinity;
        foreach (var action in playerActions)
        {
            var expectedValue = FindExpectedValue(action, gameStateDepthLimit, bestEstimate);
            bestMinimum = Math.Max(bestMinimum, expectedValue.Range.Minimum);
            bestMaximum = Math.Max(bestMaximum, expectedValue.Range.Maximum);
            bestEstimate = Math.Max(bestEstimate, expectedValue.Estimate);
        }

        // TODO: Return the true range of possible best expected values
        // If the best expected value is certain, min and max should equal estimate
        // If the best expected value is uncertain:
        //  * min should be minimum guaranteed health
        //  * max should be maximum possible health
        //
        // SOLUTION:
        // - min = max(min)
        // - max = max(max)
        return new ExpectedValue(bestEstimate, new Range(bestMinimum, bestMaximum));
    }

    private static int GetActionPriority(PlayerAction action)
    {
        if (action is PlayCardAction playCardAction)
            return playCardAction.Card switch
            {
                Bash => 3,
                Strike => 2,
                Defend => 1,
                _ => 0
            };

        return 0;
    }

    private ExpectedValue FindExpectedValue(PlayerAction action, int gameStateDepthLimit,
        double cutoffValue = double.MinValue)
    {
        Interlocked.Increment(ref EvaluatedActions);
        var possibleResultsOfAction = action.Resolve().OrderByDescending(x => x.Probability.Value).ToList();
        var possibleMaximum = possibleResultsOfAction.Max(x => x.GameState.PlayerHealth.Amount);
        if (possibleMaximum < cutoffValue) // Switch to <= when possible (currently causes bug)
        {
            Interlocked.Add(ref PrunedActionOutcomes, possibleResultsOfAction.Count);
            return new ExpectedValue(0, 0);
        }

        var firstPossibilityExpectedValue =
            FindExpectedValue(possibleResultsOfAction.First().GameState, gameStateDepthLimit);
        var accumulatedEstimate = 0.0;
        var accumulatedRange = firstPossibilityExpectedValue.Range;
        var remainingProbability = 1.0;
        for (var index = 0; index < possibleResultsOfAction.Count; index++)
        {
            var possibility = possibleResultsOfAction[index];
            var possibilityExpectedValue = FindExpectedValue(possibility.GameState, gameStateDepthLimit);
            accumulatedRange += possibilityExpectedValue.Range;
            accumulatedEstimate += possibilityExpectedValue.Estimate * possibility.Probability;
            remainingProbability -= possibility.Probability;

            var maximumPossibleEstimate = accumulatedEstimate + possibleMaximum * remainingProbability;
            if (maximumPossibleEstimate < cutoffValue)
            {
                var prunedOutcomes = possibleResultsOfAction.Count - 1 - index;
                Interlocked.Add(ref PrunedActionOutcomes, prunedOutcomes);
                return new ExpectedValue(0, 0);
            }
        }

        return new ExpectedValue(accumulatedEstimate, accumulatedRange);
    }
}

[TestFixture]
internal class SolverTests
{
    [Test]
    public void TestCache()
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = 10,
            EnemyParty = new[] { new JawWorm() },
            Energy = 3,
            Hand = new Hand(new Strike(), new Defend())
        };
        var solver = new Solver();
        var firstSearchResult = solver.FindBestAction(nonTerminalGameState);
        var secondSearchResult = solver.FindBestAction(nonTerminalGameState);
        Assert.AreEqual(firstSearchResult, secondSearchResult);
        Assert.LessOrEqual(1, solver.GameStateCacheHits);
    }

    [Test]
    public void FindBestActionThrowsExceptionForTerminalGameState()
    {
        var solver = new Solver();
        Assert.Throws<ArgumentException>(() => solver.FindBestAction(new GameState
            { PlayerHealth = 0 }));
        Assert.Throws<ArgumentException>(() => solver.FindBestAction(new GameState
            { PlayerHealth = 10, EnemyParty = new EnemyParty() }));
    }

    [Test]
    public void FindsBestActionWhenPlayerCanWinImmediately()
    {
        var gameState = new GameState
        {
            PlayerHealth = 50,
            EnemyParty = new[] { new JawWorm() },
            Energy = 3,
            Hand = new Hand(new Strike())
        };
        var solver = new Solver();
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default), action);
        Assert.AreEqual(new Range(50, 50), expectedValue.Range);
        Assert.AreEqual(50, expectedValue.Estimate);
    }

    [Test]
    public void FindsBestActionWhenPlayerCanWinNextTurn()
    {
        var gameState = new GameState
        {
            PlayerHealth = 50,
            EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } },
            DrawPile = new DrawPile(new Strike())
        };
        var solver = new Solver();
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new EndTurnAction(gameState), action);
        Assert.AreEqual(new Range(39, 39), expectedValue.Range);
        Assert.AreEqual(39, expectedValue.Estimate);
    }

    [Test]
    public void FindsBestActionWhenPlayerCanWinNextTurnWithDefendInHand()
    {
        var gameState = new GameState
        {
            PlayerHealth = 50,
            EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } },
            Energy = 3,
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        var solver = new Solver();
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayUntargetedCardAction(gameState, new Defend()), action);
        Assert.AreEqual(new Range(44, 44), expectedValue.Range);
        Assert.AreEqual(44, expectedValue.Estimate);
    }

    [Test]
    public void FindsBestActionWhenOutcomeIsUncertain()
    {
        var gameState = new GameState
        {
            PlayerHealth = 50,
            EnemyParty = new[] { new JawWorm { Health = 13, IntendedMove = new Chomp() } },
            BaseEnergy = 1,
            Energy = 1,
            Hand = new Hand(new Defend(), new Strike())
        };
        var solver = new Solver { GameStateSearchDepth = 7 };
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default), action);
        Assert.LessOrEqual(0, expectedValue.Range.Minimum);
        Assert.LessOrEqual(expectedValue.Range.Maximum, 39);
        AssertRangeContains(expectedValue.Range, expectedValue.Estimate);
    }

    [Test]
    public void NarrowsExpectedValueRangeWhenSearchDepthIncreases()
    {
        var gameState = new GameState
        {
            PlayerHealth = 50,
            EnemyParty = new[] { new JawWorm { Health = 13, IntendedMove = new Chomp() } },
            BaseEnergy = 1,
            Energy = 1,
            Hand = new Hand(new Defend(), new Strike())
        };
        var (action4, expectedValue4) = new Solver { GameStateSearchDepth = 4 }.FindBestAction(gameState);
        var (action5, expectedValue5) = new Solver { GameStateSearchDepth = 5 }.FindBestAction(gameState);
        var (action6, expectedValue6) = new Solver { GameStateSearchDepth = 6 }.FindBestAction(gameState);
        var (action7, expectedValue7) = new Solver { GameStateSearchDepth = 7 }.FindBestAction(gameState);
        var expectedAction = new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default);
        Assert.AreEqual(expectedAction, action4);
        Assert.AreEqual(expectedAction, action5);
        Assert.AreEqual(expectedAction, action6);
        Assert.AreEqual(expectedAction, action7);
        AssertRangeContains(expectedValue4.Range, expectedValue5.Range);
        AssertRangeContains(expectedValue5.Range, expectedValue6.Range);
        // FAILS - SHOULD SUCCEED:
        // AssertRangeContains(expectedValue6.Range, expectedValue7.Range);
    }

    private void AssertRangeContains(Range range, double value)
    {
        Assert.True(range.Minimum <= value && value <= range.Maximum, $"{range} does not contain {value}");
    }

    private void AssertRangeContains(Range range, Range otherRange)
    {
        Assert.True(range.Minimum <= otherRange.Minimum && otherRange.Maximum <= range.Maximum,
            $"{range} does not contain {otherRange}");
    }
}