using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards.Defend;

[TestFixture]
public class DefendActionTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(new DefendCard()),
            DiscardPile = new DiscardPile(),
        };
    }


    [Test]
    public void EnemiesMustExist()
    {
        var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
        Assert.Throws<ArgumentException>(() => new PlayCardAction(gameState, new DefendCard()));
    }

    [Test]
    public void HandMustContainDefend()
    {
        var gameState = CreateBasicGameState() with { Hand = new Hand() };
        Assert.Throws<ArgumentException>(() => new PlayCardAction(gameState, new DefendCard()));
    }

    [Test]
    public void PlayerMustBeAlive()
    {
        var gameState = CreateBasicGameState() with { PlayerHealth = new Health(0) };
        Assert.Throws<ArgumentException>(() => new PlayCardAction(gameState, new DefendCard()));
    }

    [Test]
    public void EnergyMustBeAtLeastOne()
    {
        var gameState = CreateBasicGameState() with { Energy = new Energy(0) };
        Assert.Throws<ArgumentException>(() => new PlayCardAction(gameState, new DefendCard()));
    }

    [Test]
    public void ActionsWithSameGameStatesAreEqual()
    {
        var gameState = CreateBasicGameState();
        Assert.AreEqual(new PlayCardAction(gameState, new DefendCard()), new PlayCardAction(gameState, new DefendCard()));
    }

    [Test]
    public void ActionsWithDifferentGameStatesAreNotEqual()
    {
        var gameState1 = CreateBasicGameState();
        var gameState2 = CreateBasicGameState() with { Turn = new Turn(2) };
        Assert.AreNotEqual(new PlayCardAction(gameState1, new DefendCard()),
            new PlayCardAction(gameState2, new DefendCard()));
    }

    [Test]
    [TestCase(0, 5)]
    [TestCase(2, 7)]
    public void AddsPlayerArmorAndRemovesDefendCard(int initialAmountOfArmor, int expectedAmountOfArmor)
    {
        var gameState = new GameState()
        {
            PlayerArmor = new Armor(initialAmountOfArmor),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(new DefendCard()),
            DiscardPile = new DiscardPile()
        };
        var defendAction = new PlayCardAction(gameState, new DefendCard());
        var resolvedStates = defendAction.ResolveToPossibleStates();
        var expectedState = new GameState()
        {
            PlayerArmor = new Armor(expectedAmountOfArmor),
            Energy = new Energy(2),
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new DefendCard())
        };
        Assert.AreEqual(expectedState, resolvedStates.Single());
    }
}
