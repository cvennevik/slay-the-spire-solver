using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using System;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class MoveCardFromHandToDiscardPileTests : GameStateTests
{
    [Test]
    public void Test1()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard(), new StrikeCard()),
            DiscardPile = new DiscardPile()
        };
        var newGameState = gameState.MoveCardFromHandToDiscardPile(new StrikeCard());
        var expectedGameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DiscardPile = new DiscardPile(new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void Test2()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard(), new StrikeCard()),
            DiscardPile = new DiscardPile(new StrikeCard())
        };
        var newGameState = gameState.MoveCardFromHandToDiscardPile(new StrikeCard());
        var expectedGameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            DiscardPile = new DiscardPile(new StrikeCard(), new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void Test3()
    {
        var gameState = new GameState { Hand = new Hand(new StrikeCard()) };
        Assert.Throws<ArgumentException>(() => gameState.MoveCardFromHandToDiscardPile(new DefendCard()));
    }
}
