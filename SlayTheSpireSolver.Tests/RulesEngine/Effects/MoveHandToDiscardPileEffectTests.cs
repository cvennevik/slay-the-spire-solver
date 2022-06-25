using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class MoveHandToDiscardPileEffectTests
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

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new MoveHandToDiscardPileEffect(), new MoveHandToDiscardPileEffect());
    }
}