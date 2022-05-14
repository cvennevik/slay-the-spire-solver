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
        var gameState = new GameState { Turn = new Turn(initialTurnNumber) };
        var endTurnAction = gameState.GetLegalActions().First();
        GameState newGameState = endTurnAction.Resolve();
        Assert.AreEqual(new Turn(expectedTurnNumber), newGameState.Turn);
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
        var gameState = new GameState { Turn = new Turn(1) };
        var differentGameState = new GameState { Turn = new Turn(2) };
        var action1 = new EndTurnAction(gameState);
        var action2 = new EndTurnAction(differentGameState);
        Assert.AreNotEqual(action1, action2);
    }
}
