using NUnit.Framework;
using System.Linq;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class EndTurnActionTests
{
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void TestEndTurn(int initialTurnNumber, int expectedTurnNumber)
    {
        var gameState = new GameState { TurnNumber = new TurnNumber(initialTurnNumber) };
        var endTurnAction = gameState.GetLegalActions().First();
        GameState newGameState = endTurnAction.Resolve();
        Assert.AreEqual(new TurnNumber(expectedTurnNumber), newGameState.TurnNumber);
    }

    [Test]
    public void TestEquality1()
    {
        var gameState = new GameState();
        var action1 = new EndTurnAction(gameState);
        var action2 = new EndTurnAction(gameState);
        Assert.AreEqual(action1, action2);
    }

    [Test]
    public void TestEquality2()
    {
        var gameState = new GameState();
        var identicalGameState = new GameState();
        var action1 = new EndTurnAction(gameState);
        var action2 = new EndTurnAction(identicalGameState);
        Assert.AreEqual(action1, action2);
    }

    [Test]
    public void TestEquality3()
    {
        var gameState = new GameState { TurnNumber = new TurnNumber(1) };
        var differentGameState = new GameState { TurnNumber = new TurnNumber(2) };
        var action1 = new EndTurnAction(gameState);
        var action2 = new EndTurnAction(differentGameState);
        Assert.AreNotEqual(action1, action2);
    }
}
