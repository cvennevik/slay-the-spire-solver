using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class EndTurnEffectTests
{
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void Test(int initialTurn, int expectedTurn)
    {
        var gameState = new GameState { Turn = initialTurn };
        var effect = new EndTurnEffect();
        var newGameState = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = gameState with { Turn = expectedTurn };
        Assert.AreEqual(expectedGameState, newGameState);
    }
}