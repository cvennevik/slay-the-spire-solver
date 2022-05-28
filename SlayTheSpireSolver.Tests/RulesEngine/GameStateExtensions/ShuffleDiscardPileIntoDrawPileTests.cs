using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
internal class ShuffleDiscardPileIntoDrawPileTests
{
    [Test]
    public void ShufflesStrikeCardIntoEmptyDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new StrikeCard()),
            DrawPile = new DrawPile()
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void ShufflesDefendCardIntoEmptyDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new DefendCard()),
            DrawPile = new DrawPile()
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new DefendCard())
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
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard())
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        Assert.AreEqual(gameState, newGameState);
    }

    [Test]
    public void ShufflesStrikeCardIntoExistingDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new StrikeCard()),
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard())
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void ShufflesMultipleCardsIntoExistingDrawPile()
    {
        var gameState = new GameState
        {
            DiscardPile = new DiscardPile(new StrikeCard(), new DefendCard(), new DefendCard()),
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard())
        };
        var newGameState = gameState.ShuffleDiscardPileIntoDrawPile();
        var expectedGameState = new GameState
        {
            DiscardPile = new DiscardPile(),
            DrawPile = new DrawPile(new StrikeCard(), new StrikeCard(), new StrikeCard(),
                new DefendCard(), new DefendCard())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }
}
