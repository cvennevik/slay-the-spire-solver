using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class IncrementTurnEffectTests
{
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void Test(int initialTurn, int expectedTurn)
    {
        var gameState = new GameState { PlayerHealth = 21, Turn = initialTurn };
        var effect = new IncrementTurnEffect();
        var newGameState = effect.Resolve(gameState).AsSingleStableGameState();
        var expectedGameState = gameState with { Turn = expectedTurn };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new IncrementTurnEffect(), new IncrementTurnEffect());
    }
}