using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Enemies;
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
            Hand = new Hand(new StrikeCard())
        };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void MustHaveStrikeCardInHand()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm()),
        };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void MustNotHaveLost()
    {
        var gameState = new GameState
        {
            Player = new Player { Health = new Health(0) },
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(new StrikeCard())
        };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void ReducesEnemyHealthAndRemovesCardFromHand()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10) }),
        };

        var strikeAction = new StrikeAction(gameState);
        var resolvedGameState = strikeAction.Resolve();

        var expectedGameState = new GameState()
        {
            Hand = new Hand(),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(4) }),
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }
}
