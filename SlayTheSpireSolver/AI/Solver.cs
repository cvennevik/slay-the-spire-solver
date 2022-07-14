using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;

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
        return gameState.PlayerHealth.Amount;
    }
}

[TestFixture]
internal class SolverTests
{
    [Test]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    public void GetOutcomeValueReturnsPlayerHealth(int playerHealth, int expectedOutcomeValue)
    {
        var gameState = new GameState { PlayerHealth = playerHealth };
        Assert.AreEqual(expectedOutcomeValue, Solver.GetOutcomeValue(gameState));
    }
}