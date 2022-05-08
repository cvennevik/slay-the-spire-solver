using NUnit.Framework;

namespace SlayTheSpireSolver.Tests;

public class GameStateTests
{
    [Test]
    public void TestEquality1()
    {
        var gameState1 = new GameState();
        var gameState2 = new GameState();
        Assert.AreEqual(gameState1, gameState2);
    }

    [Test]
    public void TestEquality2()
    {
        var gameState1 = new GameState { Enemies = new Enemy[] { } };
        var gameState2 = new GameState { Enemies = new Enemy[] { } };
        Assert.AreEqual(gameState1, gameState2);
    }

    [Test]
    public void TestEquality3()
    {
        var gameState1 = new GameState { Enemies = new[] { new Enemy() } };
        var gameState2 = new GameState { Enemies = new Enemy[] { } };
        Assert.AreNotEqual(gameState1, gameState2);
    }

    [Test]
    public void TestEquality4()
    {
        var gameState1 = new GameState { Enemies = new[] { new Enemy() } };
        var gameState2 = new GameState { Enemies = new[] { new Enemy() } };
        Assert.AreEqual(gameState1, gameState2);
    }
}
