using System.Linq;
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
        Assert.AreEqual(gameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void RemovesStrikeFromHandAndAddsToDiscardPile()
    {
        var removeFromHand = new RemoveCardFromHandEffect(new StrikeCard());
        var addToDiscard = new AddCardToDiscardPileEffect(new StrikeCard());
        var effect = new CombinedEffect(removeFromHand, addToDiscard);

        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DiscardPile = new DiscardPile()
        };
        var expectedGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void TestNoEffectEquality()
    {
        Assert.AreEqual(new CombinedEffect(), new CombinedEffect());
    }

    [Test]
    public void TestSingleEffectEquality()
    {
        var removeFromHand = new RemoveCardFromHandEffect(new StrikeCard());
        var addToDiscard = new AddCardToDiscardPileEffect(new StrikeCard());
        Assert.AreEqual(new CombinedEffect(removeFromHand), new CombinedEffect(removeFromHand));
        Assert.AreEqual(new CombinedEffect(addToDiscard), new CombinedEffect(addToDiscard));
        Assert.AreNotEqual(new CombinedEffect(removeFromHand), new CombinedEffect(addToDiscard));
    }

    [Test]
    public void TestTwoEffectsEquality()
    {
        var removeFromHand = new RemoveCardFromHandEffect(new StrikeCard());
        var addToDiscard = new AddCardToDiscardPileEffect(new StrikeCard());
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
        var removeFromHand = new RemoveCardFromHandEffect(new StrikeCard());
        Assert.AreEqual(new CombinedEffect(removeFromHand),
            new CombinedEffect(new CombinedEffect(removeFromHand)));
        Assert.AreEqual(new CombinedEffect(removeFromHand),
            new CombinedEffect(new CombinedEffect(new CombinedEffect(removeFromHand))));
    }
}