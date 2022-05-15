using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Enemies;
using SlayTheSpireSolver.Enemies.JawWorms;
using System;

namespace SlayTheSpireSolver.Tests.Cards.Strike;

[TestFixture]
public class StrikeActionTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(new StrikeCard()),
            Turn = new Turn(1)
        };
    }

    [Test]
    public void EnemiesMustExist()
    {
        var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void HandMustContainStrike()
    {
        var gameState = CreateBasicGameState() with { Hand = new Hand() };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void PlayerMustBeAlive()
    {
        var gameState = CreateBasicGameState() with { PlayerHealth = new Health(0) };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void EnergyMustBeAtLeastOne()
    {
        var gameState = CreateBasicGameState() with { Energy = new Energy(0) };
        Assert.Throws<ArgumentException>(() => new StrikeAction(gameState));
    }

    [Test]
    public void ReducesEnemyHealthAndRemovesCardFromHand()
    {
        var initialGameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard()),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10) }),
        };

        var strikeAction = new StrikeAction(initialGameState);
        var resolvedGameState = strikeAction.Resolve();

        var expectedGameState = CreateBasicGameState() with
        {
            Hand = new Hand(),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(4) }),
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void KillsEnemy()
    {
        var initialGameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard()),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(6) }),
        };

        var strikeAction = new StrikeAction(initialGameState);
        var resolvedGameState = strikeAction.Resolve();

        var expectedGameState = CreateBasicGameState() with
        {
            Hand = new Hand(),
            EnemyParty = new EnemyParty(),
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }
}
