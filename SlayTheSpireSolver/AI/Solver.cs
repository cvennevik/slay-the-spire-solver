﻿using System.Collections.Concurrent;
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

        var bestExpectedValue = new ExpectedValue(double.NegativeInfinity, double.NegativeInfinity);
        var playerActions = gameState.GetLegalActions().OrderByDescending(GetActionPriority);
        foreach (var action in playerActions)
        {
            var cutoffValue = bestExpectedValue.Range.Minimum;
            var expectedValue = FindExpectedValue(action, gameStateDepthLimit - 1, cutoffValue);
            if (expectedValue.Estimate > bestExpectedValue.Estimate)
                bestExpectedValue = expectedValue;
        }

        return bestExpectedValue;
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
    public void FindsBestActionWhenPlayerCanWinInTwoTurns()
    {
        var gameState = new GameState
        {
            PlayerHealth = 50,
            EnemyParty = new[] { new JawWorm { Health = 13, IntendedMove = new Chomp() } },
            BaseEnergy = 1,
            Energy = 1,
            Hand = new Hand(new Defend(), new Strike())
        };
        var solver = new Solver { GameStateSearchDepth = 6 };
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default), action);
        Assert.LessOrEqual(0, expectedValue.Range.Minimum);
        Assert.Less(expectedValue.Range.Minimum, expectedValue.Estimate);
        Assert.Less(expectedValue.Estimate, expectedValue.Range.Maximum);
        Assert.LessOrEqual(expectedValue.Range.Maximum, 39);
    }
}