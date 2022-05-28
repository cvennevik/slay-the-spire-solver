using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using System.Linq;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class DrawCardTests
{
    [Test]
    public void DoesNotDrawCardWhenDrawPileAndDiscardPileEmpty()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
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
            DrawPile = new DrawPile(new StrikeCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
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
            DrawPile = new DrawPile(new StrikeCard(), new DefendCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState1 = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DrawPile = new DrawPile(new DefendCard())
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new DefendCard()),
            DrawPile = new DrawPile(new StrikeCard())
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
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard(),
                new DefendCard(), new DefendCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState1 = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(),
                new DefendCard(), new DefendCard())
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new DefendCard()),
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard(),
                new DefendCard())
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
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DrawPile = new DrawPile(new StrikeCard())
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }

    [Test]
    public void AddsCardsToExistingHandInSinglePossibleState()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DrawPile = new DrawPile(new DefendCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new StrikeCard(), new DefendCard()),
            DrawPile = new DrawPile()
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }

    [Test]
    public void AddsCardsToExistingHandInAllPossibleStates()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DrawPile = new DrawPile(new StrikeCard(), new DefendCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState1 = new GameState
        {
            Hand = new Hand(new StrikeCard(), new StrikeCard()),
            DrawPile = new DrawPile(new DefendCard())
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new StrikeCard(), new DefendCard()),
            DrawPile = new DrawPile(new StrikeCard())
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
            Hand = new Hand(new StrikeCard()),
            DrawPile = new DrawPile(),
            DiscardPile = new DiscardPile(new DefendCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState = new GameState
        {
            Hand = new Hand(new StrikeCard(), new DefendCard()),
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
            Hand = new Hand(new StrikeCard()),
            DrawPile = new DrawPile(),
            DiscardPile = new DiscardPile(new StrikeCard(), new DefendCard(), new DefendCard())
        };
        var resolvedStates = gameState.DrawCard();
        var expectedState1 = new GameState
        {
            Hand = new Hand(new StrikeCard(), new StrikeCard()),
            DrawPile = new DrawPile(new DefendCard(), new DefendCard()),
            DiscardPile = new DiscardPile()
        };
        var expectedState2 = new GameState
        {
            Hand = new Hand(new StrikeCard(), new DefendCard()),
            DrawPile = new DrawPile(new StrikeCard(), new DefendCard()),
            DiscardPile = new DiscardPile()
        };
        Assert.AreEqual(2, resolvedStates.Count);
        Assert.Contains(expectedState1, resolvedStates.ToList());
        Assert.Contains(expectedState2, resolvedStates.ToList());
    }
}
