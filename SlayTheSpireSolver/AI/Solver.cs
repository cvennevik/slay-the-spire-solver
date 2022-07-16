using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.AI;

public static class Solver
{
    // TODO:
    //  * Improve non-terminal game state estimation
    //  * Prune
    //      * Add non-terminal game state ranges
    //  * Memoize
    //  * Return more data (helps testing and observability)
    //      * Count evaluated game states

    public static SearchResult FindBestExpectedOutcome(GameState gameState, int depthLimit = 2)
    {
        if (gameState.IsCombatOver())
            return new SearchResult
            {
                ExpectedValue = Math.Max(gameState.PlayerHealth.Amount, 0),
                EvaluatedGameStates = 1
            };
        if (gameState.Turn.Number > depthLimit) return new SearchResult { ExpectedValue = 0, EvaluatedGameStates = 1 };

        var bestActionValue = double.NegativeInfinity;
        var evaluatedGameStates = 1; // This
        foreach (var action in gameState.GetLegalActions())
        {
            var willExceedTurnLimit = gameState.Turn.Number == depthLimit && action is EndTurnAction;
            var searchResult = willExceedTurnLimit
                ? new SearchResult { ExpectedValue = 0 }
                : FindBestExpectedOutcome(action, depthLimit);
            evaluatedGameStates += searchResult.EvaluatedGameStates;
            if (searchResult.ExpectedValue > bestActionValue) bestActionValue = searchResult.ExpectedValue;
        }

        return new SearchResult { ExpectedValue = bestActionValue, EvaluatedGameStates = evaluatedGameStates };
    }

    private static SearchResult FindBestExpectedOutcome(PlayerAction action, int depthLimit)
    {
        return action
            .Resolve()
            .Aggregate(new SearchResult(), (aggregate, x) =>
            {
                var searchResult = FindBestExpectedOutcome(x.GameState, depthLimit);
                return new SearchResult
                {
                    ExpectedValue = aggregate.ExpectedValue + searchResult.ExpectedValue * x.Probability.Value,
                    EvaluatedGameStates = aggregate.EvaluatedGameStates + searchResult.EvaluatedGameStates
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
        var searchResult = Solver.FindBestExpectedOutcome(terminalGameState);
        Assert.AreEqual(expectedOutcomeValue, searchResult.ExpectedValue);
        Assert.AreEqual(1, searchResult.EvaluatedGameStates);
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
        var searchResult = Solver.FindBestExpectedOutcome(terminalGameState);
        Assert.AreEqual(expectedOutcomeValue, searchResult.ExpectedValue);
        Assert.AreEqual(1, searchResult.EvaluatedGameStates);
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
        var searchResult = Solver.FindBestExpectedOutcome(nonTerminalGameState);
        Assert.AreEqual(expectedResult, searchResult.ExpectedValue);
        Assert.Less(1, searchResult.EvaluatedGameStates);
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
        var searchResult = Solver.FindBestExpectedOutcome(nonTerminalGameState);
        Assert.AreEqual(expectedResult, searchResult.ExpectedValue);
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
        var searchResult = Solver.FindBestExpectedOutcome(nonTerminalGameState);
        Assert.AreEqual(expectedResult, searchResult.ExpectedValue);
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
        var searchResult = Solver.FindBestExpectedOutcome(gameState);
        Assert.AreEqual(0, searchResult.ExpectedValue);
    }
}
