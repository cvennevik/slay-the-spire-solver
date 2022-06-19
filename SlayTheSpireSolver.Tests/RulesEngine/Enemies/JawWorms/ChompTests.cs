using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies.JawWorms;

[TestFixture]
public class ChompTests
{
    private Chomp Chomp { get; } = new Chomp();

    [Test]
    [TestCase(70, 58)]
    [TestCase(12, 0)]
    [TestCase(5, -7)]
    public void TestWithoutArmor(int initialPlayerHealth, int expectedPlayerHealth)
    {
        var gameState = new GameState { PlayerHealth = initialPlayerHealth, PlayerArmor = 0 };
        var resolvedGameState = Chomp.Resolve(gameState);
        var expectedGameState = new GameState { PlayerHealth = expectedPlayerHealth, PlayerArmor = 0 };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void TestWithArmor1()
    {
        var gameState = new GameState { PlayerHealth = 50, PlayerArmor = 10 };
        var resolvedGameState = Chomp.Resolve(gameState);
        var expectedGameState = new GameState { PlayerHealth = 48, PlayerArmor = 0 };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void TestWithArmor2()
    {
        var gameState = new GameState { PlayerHealth = 50, PlayerArmor = 20 };
        var resolvedGameState = Chomp.Resolve(gameState);
        var expectedGameState = new GameState { PlayerHealth = 50, PlayerArmor = 8 };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void TestEffect()
    {
        
    }
}
