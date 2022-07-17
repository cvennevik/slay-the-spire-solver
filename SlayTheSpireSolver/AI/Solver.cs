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
    private readonly ConcurrentDictionary<GameState, ExpectedValueRange> _gameStateCache = new();
    public int EvaluatedActions;
    public int EvaluatedGameStates;
    public int GameStateCacheHits;
    public int PrunedActionOutcomes;
    public int GameStateSearchDepth { get; init; } = 3;

    // TODO:
    //  * Parallelize
    //  * Improve non-terminal game state estimation
    //  * Improve Rules Engine performance

    public double FindExpectedValue(GameState gameState) =>
        FindExpectedValueRange(gameState, GameStateSearchDepth).ToExpectedValue;

    public (PlayerAction, double) FindBestAction(GameState gameState)
    {
        if (gameState.IsCombatOver()) throw new ArgumentException("Cannot find best actions for terminal game states");

        var actions = gameState.GetLegalActions().OrderByDescending(GetActionPriority).ToList();
        var firstAction = actions.First();
        var firstActionValueRange = FindExpectedValueRange(firstAction, GameStateSearchDepth, 0);
        var bestAction = firstAction;
        var bestActionValueRange = firstActionValueRange;
        var remainingActions = actions.Except(new[] { bestAction });
        foreach (var action in remainingActions)
        {
            var actionValueRange = FindExpectedValueRange(action, GameStateSearchDepth, bestActionValueRange.Minimum);
            if (actionValueRange.ToExpectedValue > bestActionValueRange.ToExpectedValue)
            {
                bestAction = action;
                bestActionValueRange = actionValueRange;
            }
        }

        return (bestAction, bestActionValueRange.Minimum);
    }

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
        var playerActions = gameState.GetLegalActions().OrderByDescending(GetActionPriority);
        foreach (var action in playerActions)
        {
            var actionValueRange =
                FindExpectedValueRange(action, gameStateDepthLimit - 1, bestActionValueRange.Minimum);
            if (actionValueRange.ToExpectedValue > bestActionValueRange.ToExpectedValue)
                bestActionValueRange = actionValueRange;
        }

        return bestActionValueRange;
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

    private ExpectedValueRange FindExpectedValueRange(PlayerAction action, int gameStateDepthLimit,
        double bestCompetingMinimum)
    {
        Interlocked.Increment(ref EvaluatedActions);
        var possibleResultsOfAction = action.Resolve().OrderByDescending(x => x.Probability.Value).ToList();
        var bestPossibleValue = possibleResultsOfAction.Max(x => x.GameState.PlayerHealth.Amount);
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

    [Test]
    [TestCase(4)]
    public void FindsNonTrivialValue(int searchDepth)
    {
        var jawWorm = new JawWorm
        {
            Health = 26,
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
        var solver = new Solver { GameStateSearchDepth = searchDepth };
        var result = solver.FindExpectedValue(gameState);
        Assert.LessOrEqual(69, result);
        Assert.LessOrEqual(result, 74);
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
        var (action, value) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default), action);
        Assert.AreEqual(50, value);
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
        var (action, value) = solver.FindBestAction(gameState);
        Assert.AreEqual(new EndTurnAction(gameState), action);
        Assert.AreEqual(39, value);
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
        var (action, value) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayUntargetedCardAction(gameState, new Defend()), action);
        Assert.AreEqual(44, value);
    }
}
