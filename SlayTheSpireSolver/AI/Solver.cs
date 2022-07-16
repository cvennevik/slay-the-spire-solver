using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.AI;

public static class Solver
{
    // TODO:
    //  * Depth-limit
    //      * Estimate non-terminal game states
    //  * Prune
    //      * Add non-terminal game state ranges
    //  * Memoize

    public static double FindBestExpectedValue(GameState gameState)
    {
        if (gameState.IsCombatOver()) return Math.Max(gameState.PlayerHealth.Amount, 0);

        var bestActionValue = double.NegativeInfinity;
        foreach (var action in gameState.GetLegalActions())
        {
            var possibleResultsOfAction = action.Resolve();
            var actionValue = possibleResultsOfAction.Sum(x =>
                FindBestExpectedValue(x.GameState) * x.Probability.Value);
            if (actionValue > bestActionValue) bestActionValue = actionValue;
        }

        return bestActionValue;
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
        Assert.AreEqual(expectedOutcomeValue, Solver.FindBestExpectedValue(terminalGameState));
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
        Assert.AreEqual(expectedOutcomeValue, Solver.FindBestExpectedValue(terminalGameState));
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
        var result = Solver.FindBestExpectedValue(nonTerminalGameState);
        Assert.AreEqual(expectedResult, result);
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
        var result = Solver.FindBestExpectedValue(nonTerminalGameState);
        Assert.AreEqual(expectedResult, result);
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
        var result = Solver.FindBestExpectedValue(nonTerminalGameState);
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
        // DOES NOT TERMINATE:
        // var bestExpectedValue = Solver.GetOutcomeValue(gameState);
    }
}
