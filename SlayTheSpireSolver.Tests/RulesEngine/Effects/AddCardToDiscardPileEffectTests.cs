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
        var result = effect.Resolve(gameState).SingleResolvedGameState();
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
        var resultingGameState = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(new Strike(), new Defend(), new Strike())
        };
        Assert.AreEqual(expectedGameState, resultingGameState);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new AddCardToDiscardPileEffect(new Strike()),
            new AddCardToDiscardPileEffect(new Strike()));
        Assert.AreEqual(new AddCardToDiscardPileEffect(new Defend()),
            new AddCardToDiscardPileEffect(new Defend()));
        Assert.AreNotEqual(new AddCardToDiscardPileEffect(new Strike()),
            new AddCardToDiscardPileEffect(new Defend()));
    }
}