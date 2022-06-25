using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class MoveHandToDiscardPileEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { Hand = new Hand(), DiscardPile = new DiscardPile(new Strike()) };
        var effect = new MoveHandToDiscardPileEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new MoveHandToDiscardPileEffect(), new MoveHandToDiscardPileEffect());
    }
}