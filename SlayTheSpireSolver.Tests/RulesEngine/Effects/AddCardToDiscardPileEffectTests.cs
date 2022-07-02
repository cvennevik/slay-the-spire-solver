using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AddCardToDiscardPileEffectTests
{
    [Test]
    public void AddsToEmptyDiscardPile()
    {
        var gameState = new GameState { DiscardPile = new DiscardPile() };
        var effect = new AddCardToDiscardPileEffect(new Strike());
        var result = effect.Resolve(gameState).SingleResolvedState();
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
        var resultingGameState = effect.Resolve(gameState).SingleResolvedState();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(new Strike(), new Defend(), new Strike())
        };
        Assert.AreEqual(expectedGameState, resultingGameState);
    }
}