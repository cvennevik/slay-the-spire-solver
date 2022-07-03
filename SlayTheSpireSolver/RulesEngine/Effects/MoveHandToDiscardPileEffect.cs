using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record MoveHandToDiscardPileEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newDiscardPile = gameState.Hand.Cards.Aggregate(
            gameState.DiscardPile, (current, card) => current.Add(card));

        return gameState with { Hand = new Hand(), DiscardPile = newDiscardPile };
    }
}

[TestFixture]
internal class MoveHandToDiscardPileEffectTests
{
    [Test]
    public void DoesNothingWhenHandIsEmpty()
    {
        var gameState = new GameState { Hand = new Hand(), DiscardPile = new DiscardPile(new Strike()) };
        var effect = new MoveHandToDiscardPileEffect();
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void PutsCardsInHandInEmptyDiscardPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike(), new Strike(), new Defend()),
            DiscardPile = new DiscardPile()
        };
        var effect = new MoveHandToDiscardPileEffect();
        var result = effect.Resolve(gameState).SingleResolvedState();
        var expectedGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Strike(), new Strike(), new Defend())
        };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsCardsInHandToExistingDiscardPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DiscardPile = new DiscardPile(new Strike(), new Defend())
        };
        var effect = new MoveHandToDiscardPileEffect();
        var result = effect.Resolve(gameState).SingleResolvedState();
        var expectedGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Strike(), new Strike(), new Defend())
        };
        Assert.AreEqual(expectedGameState, result);
    }
}