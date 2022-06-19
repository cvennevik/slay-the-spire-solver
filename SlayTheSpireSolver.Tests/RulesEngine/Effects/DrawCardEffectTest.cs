using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DrawCardEffectTest
{
    [Test]
    public void DoesNothingWhenDrawPileAndDiscardPileEmpty()
    {
        var gameState = new GameState { Hand = new Hand(), DrawPile = new DrawPile(), DiscardPile = new DiscardPile() };
        var result = new DrawCardEffect().Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DrawsCardWhenSingleCardInDrawPile()
    {
        var gameState = new GameState { Hand = new Hand(), DrawPile = new DrawPile(new Strike()) };
        var result = new DrawCardEffect().Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState { Hand = new Hand(new Strike()), DrawPile = new DrawPile() };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void DrawsEitherCardWhenTwoCardsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(), DrawPile = new DrawPile(new Strike(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedGameState1 = new GameState
        {
            Hand = new Hand(new Strike()), DrawPile = new DrawPile(new Defend())
        };
        var expectedGameState2 = new GameState
        {
            Hand = new Hand(new Defend()), DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(2, result.Count);
        Assert.Contains(expectedGameState1.WithEffectStack(), result.ToList());
        Assert.Contains(expectedGameState2.WithEffectStack(), result.ToList());
    }

    [Test]
    public void DrawsEitherKindWhenTwoCardKindsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                new Defend(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedGameState1 = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Strike(),
                new Defend(), new Defend())
        };
        var expectedGameState2 = new GameState
        {
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                new Defend())
        };
        Assert.AreEqual(2, result.Count);
        Assert.Contains(expectedGameState1.WithEffectStack(), result.ToList());
        Assert.Contains(expectedGameState2.WithEffectStack(), result.ToList());
    }

    [Test]
    public void CanOnlyDrawOneCardWhenSameCardsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike())
        };
        var result = new DrawCardEffect().Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsCardsToExistingHandInSinglePossibleState()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile()
        };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsCardsToExistingHandInAllPossibleStates()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedState1 = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(2, result.Count);
        Assert.Contains(expectedState1.WithEffectStack(), result.ToList());
        Assert.Contains(expectedState2.WithEffectStack(), result.ToList());
    }

    [Test]
    public void ShufflesAndDrawsDiscardPileWithSingleCard()
    {
        var gameState = new GameState { Hand = new Hand(), DiscardPile = new DiscardPile(new Strike()) };
        var result = new DrawCardEffect().Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState { Hand = new Hand(new Strike()), DiscardPile = new DiscardPile() };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void ShufflesAndDrawsDiscardPileWithMultipleCards()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(),
            DiscardPile = new DiscardPile(new Strike(), new Defend(), new Defend())
        };
        var result = new DrawCardEffect().Resolve(gameState);
        var expectedState1 = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend(), new Defend()),
            DiscardPile = new DiscardPile()
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile(new Strike(), new Defend()),
            DiscardPile = new DiscardPile()
        };
        Assert.AreEqual(2, result.Count);
        Assert.Contains(expectedState1.WithEffectStack(), result.ToList());
        Assert.Contains(expectedState2.WithEffectStack(), result.ToList());
    }
}