using NUnit.Framework;
using SlayTheSpireSolver.Cards.Defend;
using System;

namespace SlayTheSpireSolver.Tests.Cards.Defend;

[TestFixture]
public class DefendActionTests
{
    [Test]
    public void MustHaveDefendCardInHand()
    {
        var gameState = new GameState();
        Assert.Throws<ArgumentException>(() => new DefendAction(gameState));
    }

    [Test]
    public void ActionsWithSameGameStatesAreEqual()
    {
        var gameState = new GameState { Hand = new Hand(new DefendCard()) };
        Assert.AreEqual(new DefendAction(gameState), new DefendAction(gameState));
    }

    [Test]
    public void ActionsWithDifferentGameStatesAreNotEqual()
    {
        var gameState1 = new GameState
        {
            Hand = new Hand(new DefendCard()),
            Turn = new Turn(1)
        };
        var gameState2 = new GameState
        {
            Hand = new Hand(new DefendCard()),
            Turn = new Turn(2)
        };
        Assert.AreNotEqual(new DefendAction(gameState1), new DefendAction(gameState2));
    }
}
