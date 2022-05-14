using NUnit.Framework;
using SlayTheSpireSolver.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.JawWorms;

[TestFixture]
public class ChompTests
{
    [Test]
    [TestCase(70, 58)]
    [TestCase(12, 0)]
    [TestCase(5, -7)]
    public void Test(int initialPlayerHealth, int expectedPlayerHealth)
    {
        var gameState = new GameState { Player = new Player { Health = new Health(initialPlayerHealth) } };
        var chomp = new Chomp();
        var resolvedGameState = chomp.Resolve(gameState);
        Assert.AreEqual(resolvedGameState, gameState with { Player = gameState.Player with { Health = new Health(expectedPlayerHealth) } });
    }
}
