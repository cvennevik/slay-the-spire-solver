﻿using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Enemies.JawWorms;
using System.Linq;

namespace SlayTheSpireSolver.Tests.Cards.Strike;

[TestFixture]
public class StrikeCardTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new StrikeCard(), new StrikeCard());
    }

    [Test]
    public void NoLegalActionsWhenNoStrikeCardInHand()
    {
        var gameState = new GameState
        {
            Enemy = new JawWorm()
        };
        var legalActions = new StrikeCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void NoLegalActionsWhenNoEnemy()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard())
        };
        var legalActions = new StrikeCard().GetLegalActions(gameState);
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void OneLegalActionWhenOneEnemy()
    {
        var gameState = new GameState
        {
            Hand = new Hand(new StrikeCard()),
            Enemy = new JawWorm()
        };
        var legalActions = new StrikeCard().GetLegalActions(gameState).ToList();
        Assert.AreEqual(1, legalActions.Count);
        Assert.AreEqual(new StrikeAction(gameState), legalActions.First());
    }
}
