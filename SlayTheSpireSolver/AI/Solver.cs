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
    public void GetOutcomeValueReturnsPlayerHealth()
    {
        var gameState = new GameState { PlayerHealth = 10 };
        Assert.AreEqual(10, Solver.GetOutcomeValue(gameState));
    }
}