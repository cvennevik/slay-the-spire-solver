using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class CombinedEffectTests
{
    [Test]
    public void DoesNothingWhenEmpty()
    {
        var effect = new CombinedEffect();
        var gameState = new GameState { PlayerHealth = new Health(42) };
        Assert.AreEqual(gameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void RemovesStrikeFromHandAndAddsToDiscardPile()
    {
        var removeFromHand = new RemoveCardFromHandEffect(new Strike());
        var addToDiscard = new AddCardToDiscardPileEffect(new Strike());
        var effect = new CombinedEffect(removeFromHand, addToDiscard);

        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DiscardPile = new DiscardPile()
        };
        var expectedGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Strike())
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void TestNoEffectEquality()
    {
        Assert.AreEqual(new CombinedEffect(), new CombinedEffect());
    }

    [Test]
    public void TestSingleEffectEquality()
    {
        var removeFromHand = new RemoveCardFromHandEffect(new Strike());
        var addToDiscard = new AddCardToDiscardPileEffect(new Strike());
        Assert.AreEqual(new CombinedEffect(removeFromHand), new CombinedEffect(removeFromHand));
        Assert.AreEqual(new CombinedEffect(addToDiscard), new CombinedEffect(addToDiscard));
        Assert.AreNotEqual(new CombinedEffect(removeFromHand), new CombinedEffect(addToDiscard));
    }

    [Test]
    public void TestTwoEffectsEquality()
    {
        var removeFromHand = new RemoveCardFromHandEffect(new Strike());
        var addToDiscard = new AddCardToDiscardPileEffect(new Strike());
        Assert.AreEqual(new CombinedEffect(removeFromHand, addToDiscard),
            new CombinedEffect(removeFromHand, addToDiscard));
        Assert.AreEqual(new CombinedEffect(addToDiscard, removeFromHand),
            new CombinedEffect(addToDiscard, removeFromHand));
        Assert.AreNotEqual(new CombinedEffect(removeFromHand, addToDiscard),
            new CombinedEffect(addToDiscard, removeFromHand));
    }

    [Test]
    public void TestNestedEquality()
    {
        var removeFromHand = new RemoveCardFromHandEffect(new Strike());
        Assert.AreEqual(new CombinedEffect(removeFromHand),
            new CombinedEffect(new CombinedEffect(removeFromHand)));
        Assert.AreEqual(new CombinedEffect(removeFromHand),
            new CombinedEffect(new CombinedEffect(new CombinedEffect(removeFromHand))));
    }
}