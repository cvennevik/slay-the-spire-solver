using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.AI;

public static class Solver
{
    // todo
    //  1. Evaluate terminal game states
    //  2. Evaluate non-terminal game states
    //  3. Evaluate actions
    //
    // Action -> GameStates
    // Non-terminal GameState -> Actions
    // Terminal GameState -> OutcomeValue

    public static double GetOutcomeValue(GameState gameState)
    {
        if (gameState.IsCombatOver()) return Math.Max(gameState.PlayerHealth.Amount, 0);

        var bestActionValue = double.NegativeInfinity;
        foreach (var action in gameState.GetLegalActions())
        {
            var possibleResultsOfAction = action.Resolve();
            var actionValue = possibleResultsOfAction.Sum(x =>
                GetOutcomeValue(x.GameState) * x.Probability.Value);
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
        Assert.AreEqual(expectedOutcomeValue, Solver.GetOutcomeValue(terminalGameState));
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
        Assert.AreEqual(expectedOutcomeValue, Solver.GetOutcomeValue(terminalGameState));
    }

    [Test]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    public void ReturnsPlayerHealthWhenPlayerCanWinImmediately(int playerHealth, int expectedOutcomeValue)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm() },
            Energy = 3,
            Hand = new Hand(new Strike(), new Defend())
        };
        var outcomeValue = Solver.GetOutcomeValue(nonTerminalGameState);
        Assert.AreEqual(expectedOutcomeValue, outcomeValue);
    }

    [Test]
    [TestCase(13, 1)]
    [TestCase(20, 8)]
    public void ReturnsPlayerHealthMinusEnemyAttackWhenPlayerCanWinNextTurn(int playerHealth, int expectedOutcomeValue)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } },
            DrawPile = new DrawPile(new Strike())
        };
        var outcomeValue = Solver.GetOutcomeValue(nonTerminalGameState);
        Assert.AreEqual(expectedOutcomeValue, outcomeValue);
    }

    [Test]
    [TestCase(13, 6)]
    [TestCase(20, 13)]
    public void ReturnsPlayerHealthPlusDefendArmorMinusEnemyAttackWhenPlayerCanWinNextTurn(int playerHealth,
        int expectedOutcomeValue)
    {
        var nonTerminalGameState = new GameState
        {
            PlayerHealth = playerHealth,
            EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } },
            Energy = 1,
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        var outcomeValue = Solver.GetOutcomeValue(nonTerminalGameState);
        Assert.AreEqual(expectedOutcomeValue, outcomeValue);
    }
}
