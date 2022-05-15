using NUnit.Framework;
using SlayTheSpireSolver.Cards.Defend;
using SlayTheSpireSolver.Enemies;
using SlayTheSpireSolver.Enemies.JawWorms;
using System;

namespace SlayTheSpireSolver.Tests.Cards.Defend;

[TestFixture]
public class DefendActionTests
{
    [Test]
    public void MustHaveDefendCardInHand()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        Assert.Throws<ArgumentException>(() => new DefendAction(gameState));
    }

    [Test]
    public void MustNotHaveWon()
    {
        var gameState = new GameState { Hand = new Hand(new DefendCard()) };
        Assert.Throws<ArgumentException>(() => new DefendAction(gameState));
    }

    [Test]
    public void MustNotHaveLost()
    {
        var gameState = new GameState
        {
            Player = new Player { Health = new Health(0) },
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(new DefendCard())
        };
        Assert.Throws<ArgumentException>(() => new DefendAction(gameState));
    }

    [Test]
    public void ActionsWithSameGameStatesAreEqual()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(new DefendCard())
        };
        Assert.AreEqual(new DefendAction(gameState), new DefendAction(gameState));
    }

    [Test]
    public void ActionsWithDifferentGameStatesAreNotEqual()
    {
        var gameState1 = new GameState
        {
            Hand = new Hand(new DefendCard()),
            EnemyParty = new EnemyParty(new JawWorm()),
            Turn = new Turn(1)
        };
        var gameState2 = new GameState
        {
            Hand = new Hand(new DefendCard()),
            EnemyParty = new EnemyParty(new JawWorm()),
            Turn = new Turn(2)
        };
        Assert.AreNotEqual(new DefendAction(gameState1), new DefendAction(gameState2));
    }

    [Test]
    [TestCase(0, 5)]
    [TestCase(2, 7)]
    public void AddsArmorToPlayerAndRemovesDefendCard(int initialArmorValue, int expectedArmorValue)
    {
        var gameState = new GameState()
        {
            Player = new Player { Armor = new Armor(initialArmorValue) },
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand(new DefendCard())
        };
        var defendAction = new DefendAction(gameState);
        var resolvedGameState = defendAction.Resolve();
        var expectedGameState = new GameState()
        {
            Player = new Player { Armor = new Armor(expectedArmorValue) },
            EnemyParty = new EnemyParty(new JawWorm()),
            Hand = new Hand()
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }
}
