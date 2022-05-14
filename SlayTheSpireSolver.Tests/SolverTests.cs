using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;
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

    [Test]
    public void Test2()
    {
        var gameState = new GameState
        {
            Player = new Player { Health = new Health(50) },
            Enemy = new JawWorm { Health = new Health(6) },
            Hand = new Hand(new StrikeCard())
        };
        var bestAction = Solver.GetBestAction(gameState);
        Assert.AreEqual(new StrikeAction(gameState), bestAction);
    }
}
