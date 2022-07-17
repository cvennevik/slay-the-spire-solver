﻿using System.Collections.Concurrent;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.AI;

public class Solver
{
    private readonly ConcurrentDictionary<GameState, ExpectedValueRange> _gameStateCache = new();
    public int EvaluatedActions;
    public int EvaluatedGameStates;
    public int GameStateCacheHits;
    public int PrunedActionOutcomes;
    public int GameStateSearchDepth { get; init; } = 3;

    // TODO:
    //  * Parallelize
    //  * Improve non-terminal game state estimation
    //  * Return best actions + their expected value
    //  * Check correctness
    //      * Write tests for game states solvable in few steps
    //      * Try playing a real game using the solver
    //  * Improve Rules Engine performance
    //  * Improve action evaluation order (goal: hit winning paths earlier to improve pruning)

    public double FindExpectedValue(GameState gameState) =>
        FindExpectedValueRange(gameState, GameStateSearchDepth).ToExpectedValue;

    private ExpectedValueRange FindExpectedValueRange(GameState gameState, int gameStateDepthLimit)
    {
        var isCached = _gameStateCache.TryGetValue(gameState, out var cachedResult);
        if (isCached)
        {
            Interlocked.Increment(ref GameStateCacheHits);
            return cachedResult!;
        }

        Interlocked.Increment(ref EvaluatedGameStates);
        var result = FindExpectedValueRangeUncached(gameState, gameStateDepthLimit);
        _gameStateCache.TryAdd(gameState, result);
        return result;
    }

    private ExpectedValueRange FindExpectedValueRangeUncached(GameState gameState, int gameStateDepthLimit)
    {
        if (gameState.IsCombatOver())
        {
            var result = Math.Max(gameState.PlayerHealth.Amount, 0);
            return new ExpectedValueRange(result, result);
        }

        if (gameStateDepthLimit <= 0) return new ExpectedValueRange(0, gameState.PlayerHealth.Amount);

        var bestActionValueRange = new ExpectedValueRange(double.NegativeInfinity, double.NegativeInfinity);
        var bestPossibleValue = gameState.PlayerHealth.Amount;
        var playerActions = gameState.GetLegalActions().OrderByDescending(GetActionPriority);
        foreach (var action in playerActions)
        {
            var actionValueRange = FindExpectedValueRange(action, gameStateDepthLimit - 1,
                bestPossibleValue, bestActionValueRange.Minimum);
            if (actionValueRange.ToExpectedValue > bestActionValueRange.ToExpectedValue)
                bestActionValueRange = actionValueRange;
        }

        return bestActionValueRange;
    }

    private ExpectedValueRange FindExpectedValueRange(PlayerAction action, int gameStateDepthLimit,
        double bestPossibleValue, double bestCompetingMinimum)
    {
        Interlocked.Increment(ref EvaluatedActions);
        var possibleResultsOfAction = action.Resolve().OrderByDescending(x => x.Probability.Value).ToList();
        var remainingProbability = 1.0;
        var remainingPossibilities = possibleResultsOfAction.Count;
        var aggregatedMinimum = 0.0;
        var aggregatedMaximum = 0.0;
        foreach (var possibility in possibleResultsOfAction)
        {
            remainingProbability -= possibility.Probability.Value;
            var possibilityValueRange = FindExpectedValueRange(possibility.GameState, gameStateDepthLimit);
            aggregatedMinimum += possibilityValueRange.Minimum * possibility.Probability.Value;
            aggregatedMaximum += possibilityValueRange.Maximum * possibility.Probability.Value;
            remainingPossibilities--;

            var bestPossibleMaximum = aggregatedMaximum + bestPossibleValue * remainingProbability;
            if (bestCompetingMinimum > bestPossibleMaximum)
            {
                // The competing action's expected value is strictly higher, stop evaluating this action
                Interlocked.Add(ref PrunedActionOutcomes, remainingPossibilities);
                return new ExpectedValueRange(0, 0);
            }
        }

        return new ExpectedValueRange(aggregatedMinimum, aggregatedMaximum);
    }

    private int GetActionPriority(PlayerAction action) => 0;
}

[TestFixture]
internal class SolverTests
{
    [Test]
    [TestCase(-10, 0)]
    [TestCase(0, 0)]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    public void ReturnsPlayerHealthWhenNoEnemiesLeft(int playerHealth, int expectedResult)
    {
        var terminalGameState = new GameState { PlayerHealth = playerHealth };
        var result = new Solver().FindExpectedValue(terminalGameState);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(-10, 0)]
    public void ReturnsZeroWhenPlayerDead(int playerHealth, int expectedResult)
    {
        var terminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm() }
        };
        var result = new Solver().FindExpectedValue(terminalGameState);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    public void ReturnsPlayerHealthWhenPlayerCanWinImmediately(int playerHealth, int expectedResult)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm() },
            Energy = 3,
            Hand = new Hand(new Strike(), new Defend())
        };
        var result = new Solver().FindExpectedValue(nonTerminalGameState);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase(13, 2)]
    [TestCase(20, 9)]
    public void ReturnsPlayerHealthMinusEnemyAttackWhenPlayerCanWinNextTurn(int playerHealth, int expectedResult)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } },
            DrawPile = new DrawPile(new Strike())
        };
        var result = new Solver().FindExpectedValue(nonTerminalGameState);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase(13, 7)]
    [TestCase(20, 14)]
    public void ReturnsPlayerHealthPlusDefendArmorMinusEnemyAttackWhenPlayerCanWinNextTurn(int playerHealth,
        int expectedResult)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } },
            Energy = 1,
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        var result = new Solver().FindExpectedValue(nonTerminalGameState);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void TestFullJawWormFight()
    {
        var jawWorm = new JawWorm
        {
            Health = 44,
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
        new Solver().FindExpectedValue(gameState);
    }


    [Test]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    public void TestCache(int playerHealth, int expectedResult)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm() },
            Energy = 3,
            Hand = new Hand(new Strike(), new Defend())
        };
        var solver = new Solver();
        var firstSearchResult = solver.FindExpectedValue(nonTerminalGameState);
        var secondSearchResult = solver.FindExpectedValue(nonTerminalGameState);
        Assert.AreEqual(firstSearchResult, secondSearchResult);
        Assert.LessOrEqual(1, solver.GameStateCacheHits);
    }
}
