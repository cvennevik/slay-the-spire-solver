using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
internal class ShuffleDiscardPileIntoDrawPileTests
{
    [Test]
    public void ShufflesStrikeCardIntoEmptyDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new Strike()),
            DrawPile = new DrawPile()
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void ShufflesDefendCardIntoEmptyDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new Defend()),
            DrawPile = new DrawPile()
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new Defend())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void DoesNothingWhenBothPilesEmpty()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile()
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        Assert.AreEqual(gameState, newGameState);
    }

    [Test]
    public void DoesNothingWhenDiscardPileEmpty()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new Strike(), new Strike())
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        Assert.AreEqual(gameState, newGameState);
    }

    [Test]
    public void ShufflesStrikeCardIntoExistingDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Strike())
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void ShufflesMultipleCardsIntoExistingDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new Strike(), new Defend(), new Defend()),
            DrawPile = new DrawPile(new Strike(), new Strike())
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(),
                new Defend(), new Defend())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }
}
