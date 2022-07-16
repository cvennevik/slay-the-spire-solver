using System.Collections.Concurrent;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.AI;

public class Solver
{
    private readonly ConcurrentDictionary<GameState, SearchResult> _gameStateCache = new();
    public int CacheHits;
    public int GameStateSearchDepth { get; init; } = 3;

    // TODO:
    //  * Improve non-terminal game state estimation
    //  * Prune
    //      * Add non-terminal game state ranges
    //  * Memoize

    public SearchResult FindBestExpectedOutcome(GameState gameState)
    {
        return FindBestExpectedOutcome(gameState, GameStateSearchDepth);
    }

    private SearchResult FindBestExpectedOutcome(GameState gameState, int gameStateDepthLimit)
    {
        var isCached = _gameStateCache.TryGetValue(gameState, out var cachedResult);
        if (isCached)
        {
            Interlocked.Increment(ref CacheHits);
            return cachedResult!;
        }

        if (gameState.IsCombatOver())
        {
            var value = Math.Max(gameState.PlayerHealth.Amount, 0);
            var result = new SearchResult
            {
                ExpectedValue = value,
                EvaluatedGameStates = 1
            };
            _gameStateCache.TryAdd(gameState, result);
            return result;
        }

        if (gameStateDepthLimit <= 0) return new SearchResult { ExpectedValue = 0, EvaluatedGameStates = 1 };

        var bestActionValue = double.NegativeInfinity;
        var evaluatedGameStates = 1;
        var evaluatedActions = 0;
        foreach (var action in gameState.GetLegalActions())
        {
            var searchResult = FindBestExpectedOutcome(action, gameStateDepthLimit - 1);
            evaluatedGameStates += searchResult.EvaluatedGameStates;
            evaluatedActions += searchResult.EvaluatedActions;
            if (searchResult.ExpectedValue > bestActionValue) bestActionValue = searchResult.ExpectedValue;
        }

        return new SearchResult
        {
            ExpectedValue = bestActionValue,
            EvaluatedGameStates = evaluatedGameStates,
            EvaluatedActions = evaluatedActions
        };
    }

    private SearchResult FindBestExpectedOutcome(PlayerAction action, int gameStateDepthLimit)
    {
        var possibleResultsOfAction = action.Resolve();
        return possibleResultsOfAction.Aggregate(new SearchResult { EvaluatedActions = 1 }, (aggregate, x) =>
        {
            var searchResult = FindBestExpectedOutcome(x.GameState, gameStateDepthLimit);
            return new SearchResult
            {
                ExpectedValue = aggregate.ExpectedValue + searchResult.ExpectedValue * x.Probability.Value,
                EvaluatedGameStates = aggregate.EvaluatedGameStates + searchResult.EvaluatedGameStates,
                EvaluatedActions = aggregate.EvaluatedActions + searchResult.EvaluatedActions
            };
        });
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
    public void ReturnsPlayerHealthWhenNoEnemiesLeft(int playerHealth, int expectedOutcomeValue)
    {
        var terminalGameState = new GameState { PlayerHealth = playerHealth };
        var searchResult = new Solver().FindBestExpectedOutcome(terminalGameState);
        Assert.AreEqual(expectedOutcomeValue, searchResult.ExpectedValue);
        Assert.AreEqual(1, searchResult.EvaluatedGameStates);
        Assert.AreEqual(0, searchResult.EvaluatedActions);
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(-10, 0)]
    public void ReturnsZeroWhenPlayerDead(int playerHealth, int expectedOutcomeValue)
    {
        var terminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm() }
        };
        var searchResult = new Solver().FindBestExpectedOutcome(terminalGameState);
        Assert.AreEqual(expectedOutcomeValue, searchResult.ExpectedValue);
        Assert.AreEqual(1, searchResult.EvaluatedGameStates);
        Assert.AreEqual(0, searchResult.EvaluatedActions);
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
        var searchResult = new Solver().FindBestExpectedOutcome(nonTerminalGameState);
        Assert.AreEqual(expectedResult, searchResult.ExpectedValue);
        Assert.LessOrEqual(2, searchResult.EvaluatedGameStates);
        Assert.LessOrEqual(1, searchResult.EvaluatedActions);
    }

    [Test]
    [TestCase(13, 1)]
    [TestCase(20, 8)]
    public void ReturnsPlayerHealthMinusEnemyAttackWhenPlayerCanWinNextTurn(int playerHealth, int expectedResult)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } },
            DrawPile = new DrawPile(new Strike())
        };
        var searchResult = new Solver().FindBestExpectedOutcome(nonTerminalGameState);
        Assert.AreEqual(expectedResult, searchResult.ExpectedValue);
        Assert.LessOrEqual(3, searchResult.EvaluatedGameStates);
        Assert.LessOrEqual(2, searchResult.EvaluatedActions);
    }

    [Test]
    [TestCase(13, 6)]
    [TestCase(20, 13)]
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
        var searchResult = new Solver().FindBestExpectedOutcome(nonTerminalGameState);
        Assert.AreEqual(expectedResult, searchResult.ExpectedValue);
        Assert.LessOrEqual(4, searchResult.EvaluatedGameStates);
        Assert.LessOrEqual(3, searchResult.EvaluatedActions);
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
        new Solver().FindBestExpectedOutcome(gameState);
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
        var firstSearchResult = new Solver().FindBestExpectedOutcome(nonTerminalGameState);
        var secondSearchResult = new Solver().FindBestExpectedOutcome(nonTerminalGameState);
        Assert.AreEqual(expectedResult, firstSearchResult.ExpectedValue);
    }
}
