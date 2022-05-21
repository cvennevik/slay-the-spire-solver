using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards.Strike;

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
            DiscardPile = new DiscardPile()
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
    public void TestStrike()
    {
        var initialGameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard()),
            DiscardPile = new DiscardPile(),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10) }),
        };

        var strikeAction = new StrikeAction(initialGameState);
        var resolvedStates = strikeAction.ResolvePossibleStates();

        var expectedState = CreateBasicGameState() with
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new StrikeCard()),
            Energy = new Energy(2),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(4) }),
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }

    [Test]
    public void TestLethalStrike()
    {
        var initialGameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard()),
            DiscardPile = new DiscardPile(),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(6) }),
        };

        var strikeAction = new StrikeAction(initialGameState);
        var resolvedStates = strikeAction.ResolvePossibleStates();

        var expectedState = CreateBasicGameState() with
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new StrikeCard()),
            Energy = new Energy(2),
            EnemyParty = new EnemyParty()
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }
}
