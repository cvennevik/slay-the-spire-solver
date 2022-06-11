using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using System.Linq;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class DrawCardTests
{
    [Test]
    public void DoesNotDrawCardWhenDrawPileAndDiscardPileEmpty()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile()
        };
        var resolvedStates = gameState.DrawCard();
        Assert.AreEqual(gameState, resolvedStates.Single());
    }

    [Test]
    public void DrawsCardWhenOneCardInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile()
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }

    [Test]
    public void CanDrawEitherCardWhenTwoDifferentCardsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Defend())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState1 = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(2, resolvedStates.Count);
        Assert.Contains(expectedState1, resolvedStates.ToList());
        Assert.Contains(expectedState2, resolvedStates.ToList());
    }

    [Test]
    public void OnlyTwoPossibleStatesWhenTwoCardKindsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                new Defend(), new Defend())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState1 = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Strike(),
                new Defend(), new Defend())
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                new Defend())
        };
        Assert.AreEqual(2, resolvedStates.Count);
        Assert.Contains(expectedState1, resolvedStates.ToList());
        Assert.Contains(expectedState2, resolvedStates.ToList());
    }

    [Test]
    public void CanOnlyDrawOneCardWhenSameCardsInDrawPile()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }

    [Test]
    public void AddsCardsToExistingHandInSinglePossibleState()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile()
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }

    [Test]
    public void AddsCardsToExistingHandInAllPossibleStates()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Defend())
        };
        var resolvedStates = gameState.DrawCard();
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
        Assert.AreEqual(2, resolvedStates.Count);
        Assert.Contains(expectedState1, resolvedStates.ToList());
        Assert.Contains(expectedState2, resolvedStates.ToList());
    }

    [Test]
    public void ShufflesAndDrawsDiscardPileWithSingleCard()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(),
            DiscardPile = new DiscardPile(new Defend())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile(),
            DiscardPile = new DiscardPile()
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
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
        var resolvedStates = gameState.DrawCard();
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
        Assert.AreEqual(2, resolvedStates.Count);
        Assert.Contains(expectedState1, resolvedStates.ToList());
        Assert.Contains(expectedState2, resolvedStates.ToList());
    }
}
