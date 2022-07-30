using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.AI;

public class Solver
{
    public int GameStateSearchDepth { get; }
    public int EvaluatedGameStates { get; private set; }
    public int GameStateCacheHits { get; private set; }
    public int PrunedActionOutcomes { get; private set; }

    private readonly Dictionary<GameState, ExpectedValue> _gameStateCache = new();

    public Solver(int gameStateSearchDepth)
    {
        GameStateSearchDepth = gameStateSearchDepth;
    }

    public (PlayerAction, ExpectedValue) FindBestAction(GameState gameState)
    {
        if (!gameState.GetLegalActions().Any()) throw new ArgumentException("Game state has no legal actions");

        var gameStateDepthLimit = GameStateSearchDepth - 1;
        var actions = gameState.GetLegalActions().OrderByDescending(GetActionPriority).ToList();
        var cutoffAction = actions.First();
        var cutoffExpectedValue = FindExpectedValue(cutoffAction, gameStateDepthLimit);
        var cutoffValue = cutoffExpectedValue.Minimum;
        return actions
            .Select(action => (action, FindExpectedValue(action, gameStateDepthLimit, cutoffValue)))
            .Append((cutoffAction, cutoffExpectedValue))
            .MaxBy(tuple => tuple.Item2.Estimate);
    }

    private ExpectedValue FindExpectedValue(GameState gameState, int gameStateDepthLimit)
    {
        var isCached = _gameStateCache.TryGetValue(gameState, out var cachedResult);
        if (isCached)
        {
            GameStateCacheHits++;
            return cachedResult!;
        }

        EvaluatedGameStates++;
        var result = FindExpectedValueUncached(gameState, gameStateDepthLimit);
        _gameStateCache[gameState] = result;
        return result;
    }

    private ExpectedValue FindExpectedValueUncached(GameState gameState, int gameStateDepthLimit)
    {
        var playerHealth = Math.Max(gameState.PlayerHealth.Amount, 0);
        if (gameState.IsCombatOver()) return new ExpectedValue(playerHealth);
        if (gameStateDepthLimit <= 0) return new ExpectedValue(0, 0);

        gameStateDepthLimit -= 1;
        var playerActions = gameState.GetLegalActions().OrderByDescending(GetActionPriority).ToList();
        var bestEstimate = double.NegativeInfinity;
        var bestMinimum = double.NegativeInfinity;
        foreach (var action in playerActions)
        {
            var expectedValue = FindExpectedValue(action, gameStateDepthLimit, bestEstimate);
            bestMinimum = Math.Max(bestMinimum, expectedValue.Minimum);
            bestEstimate = Math.Max(bestEstimate, expectedValue.Estimate);
        }

        return new ExpectedValue(bestMinimum, bestEstimate);
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
        if (possibleMaximum <= cutoffValue)
        {
            PrunedActionOutcomes += possibleOutcomes.Count;
            return new ExpectedValue(double.NegativeInfinity, double.NegativeInfinity);
        }

        var firstOutcomeExpectedValue = FindExpectedValue(possibleOutcomes.First().GameState, gameStateDepthLimit);
        var accumulatedEstimate = 0.0;
        var lowestMinimum = firstOutcomeExpectedValue.Minimum;
        var remainingProbability = 1.0;
        for (var index = 0; index < possibleOutcomes.Count; index++)
        {
            var possibility = possibleOutcomes[index];
            var possibilityExpectedValue = FindExpectedValue(possibility.GameState, gameStateDepthLimit);
            lowestMinimum = Math.Min(lowestMinimum, possibilityExpectedValue.Minimum);
            accumulatedEstimate += possibilityExpectedValue.Estimate * possibility.Probability;
            remainingProbability -= possibility.Probability;

            var maximumPossibleEstimate = accumulatedEstimate + possibleMaximum * remainingProbability;
            if (maximumPossibleEstimate < cutoffValue)
            {
                var evaluatedOutcomes = index + 1;
                var prunedOutcomes = possibleOutcomes.Count - evaluatedOutcomes;
                PrunedActionOutcomes += prunedOutcomes;
                return new ExpectedValue(double.NegativeInfinity, double.NegativeInfinity);
            }
        }

        return new ExpectedValue(lowestMinimum, accumulatedEstimate);
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
        var solver = new Solver(3);
        var firstSearchResult = solver.FindBestAction(nonTerminalGameState);
        var secondSearchResult = solver.FindBestAction(nonTerminalGameState);
        Assert.AreEqual(firstSearchResult, secondSearchResult);
        Assert.LessOrEqual(1, solver.GameStateCacheHits);
    }

    [Test]
    public void FindBestActionThrowsExceptionForTerminalGameState()
    {
        var solver = new Solver(3);
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
        var solver = new Solver(3);
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default), action);
        Assert.AreEqual(50, expectedValue.Minimum);
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
        var solver = new Solver(3);
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new EndTurnAction(gameState), action);
        Assert.AreEqual(39, expectedValue.Minimum);
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
        var solver = new Solver(3);
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayUntargetedCardAction(gameState, new Defend()), action);
        Assert.AreEqual(44, expectedValue.Minimum);
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
        var solver = new Solver(7);
        var (action, expectedValue) = solver.FindBestAction(gameState);
        Assert.AreEqual(new PlayTargetedCardAction(gameState, new Strike(), EnemyId.Default), action);
        Assert.Less(0, expectedValue.Minimum);
        Assert.Less(expectedValue.Minimum, expectedValue.Estimate);
        Assert.Less(expectedValue.Estimate, 50);
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
        var actionsAndExpectedValues = FindBestActionsAndExpectedValuesForSearchDepths(gameState, 9);
        AssertExpectedValueMinimumNeverDecreasesWithDepthPerAction(actionsAndExpectedValues);
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
        var actionsAndExpectedValues = FindBestActionsAndExpectedValuesForSearchDepths(gameState, 5);
        AssertExpectedValueMinimumNeverDecreasesWithDepthPerAction(actionsAndExpectedValues);
    }

    [Test]
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

        var actionsAndExpectedValues = FindBestActionsAndExpectedValuesForSearchDepths(gameState, 6);
        AssertExpectedValueMinimumNeverDecreasesWithDepthPerAction(actionsAndExpectedValues);
    }

    private static IEnumerable<(PlayerAction, ExpectedValue)> FindBestActionsAndExpectedValuesForSearchDepths(
        GameState gameState, int searchDepth)
    {
        var depths = Enumerable.Range(1, searchDepth);
        return depths.Select(depth => new Solver(depth).FindBestAction(gameState));
    }

    private static void AssertExpectedValueMinimumNeverDecreasesWithDepthPerAction(
        IEnumerable<(PlayerAction, ExpectedValue)> actionsAndExpectedValues)
    {
        var expectedValuesByAction = actionsAndExpectedValues.GroupBy(x => x.Item1);
        foreach (var expectedValuesGroup in expectedValuesByAction)
        {
            var expectedValues = expectedValuesGroup.Select(x => x.Item2).ToList();
            for (var i = 0; i < expectedValues.Count - 1; i++)
                Assert.LessOrEqual(expectedValues[i].Minimum, expectedValues[i + 1].Minimum);
        }
    }
}