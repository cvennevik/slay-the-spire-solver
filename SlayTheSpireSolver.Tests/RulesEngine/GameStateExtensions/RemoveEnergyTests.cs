using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class RemoveEnergyTests : GameStateTests
{
    [Test]
    [TestCase(3, 2, 1)]
    [TestCase(3, 0, 3)]
    [TestCase(3, 3, 0)]
    [TestCase(3, 4, 0)]
    [TestCase(0, 0, 0)]
    public void Test(int initialAmount, int amountToRemove, int expectedAmount)
    {
        var gameState = new GameState { Energy = new Energy(initialAmount) };
        var newGameState = gameState.Remove(new Energy(amountToRemove));
        var expectedGameState = new GameState { Energy = new Energy(expectedAmount) };
        Assert.AreEqual(expectedGameState, newGameState);
    }
}
