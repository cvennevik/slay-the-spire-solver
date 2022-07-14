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
        return Math.Max(gameState.PlayerHealth.Amount, 0);
    }
}

[TestFixture]
internal class SolverTests
{
    [Test]
    [TestCase(10, 10)]
    [TestCase(20, 20)]
    [TestCase(-10, 0)]
    public void GetOutcomeValueReturnsPlayerHealthOfTerminalGameState(int playerHealth, int expectedOutcomeValue)
    {
        var terminalGameState = new GameState { PlayerHealth = playerHealth };
        Assert.AreEqual(expectedOutcomeValue, Solver.GetOutcomeValue(terminalGameState));
    }

    [Test]
    public void Test()
    {
        
    }
}