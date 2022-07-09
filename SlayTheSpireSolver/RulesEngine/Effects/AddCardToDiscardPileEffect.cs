using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddCardToDiscardPileEffect(Card CardToAdd) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newCardsInDiscardPile = gameState.DiscardPile.Cards.Append(CardToAdd).ToArray();
        return gameState with { DiscardPile = new DiscardPile(newCardsInDiscardPile) };
    }
}

[TestFixture]
internal class AddCardToDiscardPileEffectTests
{
    [Test]
    public void AddsToEmptyDiscardPile()
    {
        var gameState = new GameState { DiscardPile = new DiscardPile() };
        var effect = new AddCardToDiscardPileEffect(new Strike());
        var result = effect.NewResolve(gameState).Single().GameState;
        Assert.AreEqual(new GameState { DiscardPile = new DiscardPile(new Strike()) }, result);
    }

    [Test]
    public void AddsToDiscardPileWithExistingCards()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new Strike(), new Defend())
        };
        var effect = new AddCardToDiscardPileEffect(new Strike());
        var resultingGameState = effect.NewResolve(gameState).Single().GameState;
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(new Strike(), new Defend(), new Strike())
        };
        Assert.AreEqual(expectedGameState, resultingGameState);
    }
}