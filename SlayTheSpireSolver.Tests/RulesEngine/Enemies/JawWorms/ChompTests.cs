using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

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
        var gameState = new GameState { PlayerHealth = new Health(initialPlayerHealth), PlayerArmor = new Armor(0) };
        var resolvedGameState = Chomp.Resolve(gameState);
        var expectedGameState = new GameState { PlayerHealth = new Health(expectedPlayerHealth), PlayerArmor = new Armor(0) };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void TestWithArmor1()
    {
        var gameState = new GameState { PlayerHealth = new Health(50), PlayerArmor = new Armor(10) };
        var resolvedGameState = Chomp.Resolve(gameState);
        var expectedGameState = new GameState { PlayerHealth = new Health(48), PlayerArmor = new Armor(0) };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void TestWithArmor2()
    {
        var gameState = new GameState { PlayerHealth = new Health(50), PlayerArmor = new Armor(20) };
        var resolvedGameState = Chomp.Resolve(gameState);
        var expectedGameState = new GameState { PlayerHealth = new Health(50), PlayerArmor = new Armor(8) };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }
}
