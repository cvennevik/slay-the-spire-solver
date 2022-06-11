using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using System;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class MoveCardFromHandToDiscardPileTests : GameStateTests
{
    [Test]
    public void Test1()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DiscardPile = new DiscardPile()
        };
        var newGameState = gameState.MoveCardFromHandToDiscardPile(new Strike());
        var expectedGameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DiscardPile = new DiscardPile(new Strike())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void Test2()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DiscardPile = new DiscardPile(new Strike())
        };
        var newGameState = gameState.MoveCardFromHandToDiscardPile(new Strike());
        var expectedGameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DiscardPile = new DiscardPile(new Strike(), new Strike())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void Test3()
    {
        var gameState = new GameState { Hand = new Hand(new Strike()) };
        Assert.Throws<ArgumentException>(() => gameState.MoveCardFromHandToDiscardPile(new Defend()));
    }
}
