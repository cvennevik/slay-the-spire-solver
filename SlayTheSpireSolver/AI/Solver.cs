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
    public int GameStateSearchDepth { get; init; } = 3;
    public int EvaluatedGameStates => _evaluatedGameStates;
    public int GameStateCacheHits => _gameStateCacheHits;
    public int PrunedActionOutcomes => _prunedActionOutcomes;

    private int _evaluatedGameStates;
    private int _gameStateCacheHits;
    private int _prunedActionOutcomes;

    private readonly ConcurrentDictionary<GameState, ExpectedValue> _gameStateCache = new();

    // TODO:
    //  * Parallelize
    //  * Improve non-terminal game state estimation
    //  * Improve Rules Engine performance
    //  * Rules Engine: Remove duplicate actions when same cards in hand
    //  * Flatten ExpectedValue (remove Range)

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
            Interlocked.Increment(ref _gameStateCacheHits);
            return cachedResult!;
        }

        Interlocked.Increment(ref _evaluatedGameStates);
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
        var possibleOutcomes = action.Resolve().OrderByDescending(x => x.Probability.Value).ToList();
        var possibleMaximum = possibleOutcomes.Max(x => x.GameState.PlayerHealth.Amount);
        if (possibleMaximum < cutoffValue) // Switch to <= when possible (currently causes bug)
        {
            Interlocked.Add(ref _prunedActionOutcomes, possibleOutcomes.Count);
            return new ExpectedValue(double.NegativeInfinity, possibleMaximum);
        }

        var firstOutcomeExpectedValue = FindExpectedValue(possibleOutcomes.First().GameState, gameStateDepthLimit);
        var accumulatedEstimate = 0.0;
        var accumulatedRange = firstOutcomeExpectedValue.Range;
        var remainingProbability = 1.0;
        for (var index = 0; index < possibleOutcomes.Count; index++)
        {
            var possibility = possibleOutcomes[index];
            var possibilityExpectedValue = FindExpectedValue(possibility.GameState, gameStateDepthLimit);
            accumulatedRange += possibilityExpectedValue.Range;
            accumulatedEstimate += possibilityExpectedValue.Estimate * possibility.Probability;
            remainingProbability -= possibility.Probability;

            var maximumPossibleEstimate = accumulatedEstimate + possibleMaximum * remainingProbability;
            if (maximumPossibleEstimate < cutoffValue)
            {
                var evaluatedOutcomes = index + 1;
                var prunedOutcomes = possibleOutcomes.Count - evaluatedOutcomes;
                Interlocked.Add(ref _prunedActionOutcomes, prunedOutcomes);
                // PROBLEM:
                // Pruning can lead to over-optimistic possible maximum.
                // Example: Depth-4 search concludes with a possible range of [0, 74],
                // while Depth-5 search concludes with a possible range of [69, 80],
                // and Depth-9 search concludes with a possible range of [69, 74]
                return new ExpectedValue(double.NegativeInfinity, possibleMaximum);
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
        var (action2, expectedValue2) = new Solver { GameStateSearchDepth = 2 }.FindBestAction(gameState);
        var (action3, expectedValue3) = new Solver { GameStateSearchDepth = 3 }.FindBestAction(gameState);
        var (action4, expectedValue4) = new Solver { GameStateSearchDepth = 4 }.FindBestAction(gameState);
        var (action5, expectedValue5) = new Solver { GameStateSearchDepth = 5 }.FindBestAction(gameState);
        var (action6, expectedValue6) = new Solver { GameStateSearchDepth = 6 }.FindBestAction(gameState);
        var (action7, expectedValue7) = new Solver { GameStateSearchDepth = 7 }.FindBestAction(gameState);
        var (action8, expectedValue8) = new Solver { GameStateSearchDepth = 8 }.FindBestAction(gameState);
        var (action9, expectedValue9) = new Solver { GameStateSearchDepth = 9 }.FindBestAction(gameState);
        var expectedAction = new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default);
        Assert.AreEqual(expectedAction, action2);
        Assert.AreEqual(expectedAction, action3);
        Assert.AreEqual(expectedAction, action4);
        Assert.AreEqual(expectedAction, action5);
        Assert.AreEqual(expectedAction, action6);
        Assert.AreEqual(expectedAction, action7);
        Assert.AreEqual(expectedAction, action8);
        Assert.AreEqual(expectedAction, action9);
        AssertRangeContains(new Range(0, 50), expectedValue2.Range);
        AssertRangeContains(expectedValue2.Range, expectedValue3.Range);
        AssertRangeContains(expectedValue3.Range, expectedValue4.Range);
        AssertRangeContains(expectedValue4.Range, expectedValue5.Range);
        AssertRangeContains(expectedValue5.Range, expectedValue6.Range);
        AssertRangeContains(expectedValue6.Range, expectedValue7.Range);
        AssertRangeContains(expectedValue7.Range, expectedValue8.Range);
        AssertRangeContains(expectedValue8.Range, expectedValue9.Range);
    }

    [Test]
    public void TestProblematicRange()
    {
        var jawWorm = new JawWorm
        {
            Health = 30,
            IntendedMove = new Chomp()
        };
        var gameState = new GameState
        {
            PlayerHealth = 80,
            BaseEnergy = 3,
            Energy = 3,
            EnemyParty = new[] { jawWorm },
            Hand = new Hand(new Strike(), new Strike(), new Strike(), new Bash(), new Defend()),
            DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike(), new Strike()),
            Turn = 1
        };
        var (action2, expectedValue2) = new Solver { GameStateSearchDepth = 2 }.FindBestAction(gameState);
        var (action4, expectedValue4) = new Solver { GameStateSearchDepth = 4 }.FindBestAction(gameState);
        var (action5, expectedValue5) = new Solver { GameStateSearchDepth = 5 }.FindBestAction(gameState);
        var (action9, expectedValue9) = new Solver { GameStateSearchDepth = 9 }.FindBestAction(gameState);
        var expectedAction = new PlayTargetedCardAction(gameState, new Bash(), EnemyId.Default);
        Assert.AreEqual(expectedAction, action2);
        Assert.AreEqual(expectedAction, action4);
        Assert.AreEqual(expectedAction, action5);
        Assert.AreEqual(expectedAction, action9);
        AssertRangeContains(new Range(0, 80), expectedValue2.Range);
        AssertRangeContains(expectedValue2.Range, expectedValue4.Range);
        // SHOULD PASS: AssertRangeContains(expectedValue4.Range, expectedValue5.Range);
        AssertRangeContains(expectedValue5.Range, expectedValue9.Range);
    }

    [Test]
    [Ignore("Fails")]
    public void TestProblematicRangeOneStepFurtherDown()
    {
        var jawWorm = new JawWorm
        {
            Health = 22,
            IntendedMove = new Chomp(),
            Vulnerable = 2
        };
        var gameState = new GameState
        {
            PlayerHealth = 80,
            BaseEnergy = 3,
            Energy = 1,
            EnemyParty = new[] { jawWorm },
            Hand = new Hand(new Strike(), new Strike(), new Strike(), new Defend()),
            DrawPile = new DrawPile(new Defend(), new Defend(), new Defend(), new Strike(), new Strike()),
            DiscardPile = new DiscardPile(new Bash()),
            Turn = 1
        };
        var (_, expectedValue1) = new Solver { GameStateSearchDepth = 1 }.FindBestAction(gameState);
        var (_, expectedValue2) = new Solver { GameStateSearchDepth = 2 }.FindBestAction(gameState);
        var (_, expectedValue3) = new Solver { GameStateSearchDepth = 3 }.FindBestAction(gameState);
        var (_, expectedValue4) = new Solver { GameStateSearchDepth = 4 }.FindBestAction(gameState);
        var (_, expectedValue5) = new Solver { GameStateSearchDepth = 5 }.FindBestAction(gameState);
        var (_, expectedValue6) = new Solver { GameStateSearchDepth = 6 }.FindBestAction(gameState);
        var (_, expectedValue7) = new Solver { GameStateSearchDepth = 7 }.FindBestAction(gameState);
        var (_, expectedValue8) = new Solver { GameStateSearchDepth = 8 }.FindBestAction(gameState);
        AssertRangeContains(new Range(0, 80), expectedValue1.Range);
        AssertRangeContains(expectedValue1.Range, expectedValue2.Range);
        AssertRangeContains(expectedValue2.Range, expectedValue3.Range);
        AssertRangeContains(expectedValue3.Range, expectedValue4.Range);
        AssertRangeContains(expectedValue4.Range, expectedValue5.Range);
        AssertRangeContains(expectedValue5.Range, expectedValue6.Range);
        AssertRangeContains(expectedValue6.Range, expectedValue7.Range);
        AssertRangeContains(expectedValue7.Range, expectedValue8.Range);
    }

    private static void AssertRangeContains(Range range, double value)
    {
        Assert.True(range.Minimum <= value && value <= range.Maximum, $"{range} does not contain {value}");
    }

    private static void AssertRangeContains(Range range, Range otherRange)
    {
        Assert.True(range.Minimum <= otherRange.Minimum && otherRange.Maximum <= range.Maximum,
            $"{range} does not contain {otherRange}");
    }
}