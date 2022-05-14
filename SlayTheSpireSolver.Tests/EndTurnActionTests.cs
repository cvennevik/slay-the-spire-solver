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
        var gameState = new GameState { TurnNumber = initialTurnNumber };
        var endTurnAction = gameState.GetLegalActions().First();
        GameState newGameState = endTurnAction.Resolve();
        Assert.AreEqual(expectedTurnNumber, newGameState.TurnNumber);
    }
}
