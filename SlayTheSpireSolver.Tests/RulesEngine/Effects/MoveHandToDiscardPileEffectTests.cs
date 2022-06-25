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
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new MoveHandToDiscardPileEffect(), new MoveHandToDiscardPileEffect());
    }
}