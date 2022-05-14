using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Enemies.JawWorms;
using System;

namespace SlayTheSpireSolver.Tests.Cards.Strike;

[TestFixture]
public class StrikeActionTests
{
    [Test]
    public void MustHaveEnemy()
    {
        var gameState = new GameState
        {
            Hand = new Hand() { Cards = new[] { new StrikeCard() } }
        };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void MustHaveStrikeCardInHand()
    {
        var gameState = new GameState
        {
            Enemy = new JawWorm()
        };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void ReducesEnemyHealthAndRemovesCardFromHand()
    {
        var gameState = new GameState
        {
            Hand = new Hand() { Cards = new[] { new StrikeCard() } },
            Enemy = new JawWorm { Health = new Health(10) }
        };

        var strikeAction = new StrikeAction(gameState);
        var resolvedGameState = strikeAction.Resolve();

        var expectedGameState = new GameState()
        {
            Hand = new Hand(),
            Enemy = new JawWorm { Health = new Health(4) }
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }
}
