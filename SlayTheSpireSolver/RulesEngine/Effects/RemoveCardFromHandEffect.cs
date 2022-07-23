using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RemoveCardFromHandEffect(Card CardToRemove) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        var cardsInHand = gameState.Hand.Cards.ToList();
        cardsInHand.Remove(CardToRemove);
        return gameState with { Hand = new Hand(cardsInHand.ToArray()) };
    }
}

[TestFixture]
internal class RemoveCardFromHandEffectTests
{
    [Test]
    public void DoesNothingWhenCardNotInHand()
    {
        var gameState = new GameState { Hand = new Hand(new Strike()) };
        var effect = new RemoveCardFromHandEffect(new Defend());
        Assert.AreEqual(gameState, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void RemovesSingleCardFromHand()
    {
        var gameState = new GameState { Hand = new Hand(new Strike()) };
        var effect = new RemoveCardFromHandEffect(new Strike());
        Assert.AreEqual(new GameState { Hand = new Hand() }, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void RemovesOnlyOneCardWhenMultipleInHand()
    {
        var gameState = new GameState { Hand = new Hand(new Strike(), new Strike()) };
        var effect = new RemoveCardFromHandEffect(new Strike());
        Assert.AreEqual(new GameState { Hand = new Hand(new Strike()) }, effect.Resolve(gameState).Single().GameState);
    }
}