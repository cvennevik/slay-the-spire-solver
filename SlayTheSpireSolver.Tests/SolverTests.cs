using NUnit.Framework;
using SlayTheSpireSolver.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class SolverTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState
        {
            Player = new Player { Health = new Health(50) },
            Enemy = new JawWorm()
        };
        var bestAction = Solver.GetBestAction(gameState);
        Assert.AreEqual(new EndTurnAction(gameState), bestAction);
    }
}
