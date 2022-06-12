using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

public class CommonCardTests<TCard> where TCard : ICard, new()
{
    protected readonly TCard Card;
    protected readonly GameState BasicGameState;

    protected CommonCardTests()
    {
        Card = new TCard();
        BasicGameState = new GameState
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(Card),
            DiscardPile = new DiscardPile(),
        };
    }

    [Test]
    public void InstancesAreEqual()
    {
        Assert.AreEqual(new TCard(), new TCard());
    }

    [Test]
    public void PlayCardActionsWithSameGameStateAreEqual()
    {
        Assert.AreEqual(new PlayCardAction(BasicGameState, Card), new PlayCardAction(BasicGameState, Card));
    }

    [Test]
    public void PlayCardActionsWithDifferentGameStatesAreNotEqual()
    {
        Assert.AreNotEqual(new PlayCardAction(BasicGameState, Card),
            new PlayCardAction(BasicGameState with {Turn = new Turn(2)}, Card));
    }

    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var legalActions = Card.GetLegalActions(BasicGameState);
        Assert.AreEqual(new PlayCardAction(BasicGameState, Card), legalActions.Single());
    }

    [Test]
    public void NoLegalActionsWhenNoEnemies()
    {
        AssertNoLegalActions(BasicGameState with { EnemyParty = new EnemyParty() });
    }

    [Test]
    public void NoLegalActionsWhenCardNotInHand()
    {
        AssertNoLegalActions(BasicGameState with { Hand = new Hand() });
    }

    [Test]
    public void NoLegalActionsWhenPlayerDefeated()
    {
        AssertNoLegalActions(BasicGameState with { PlayerHealth = new Health(0) });
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        AssertNoLegalActions(BasicGameState with { Energy = new Energy(0) });
    }

    private void AssertNoLegalActions(GameState gameState)
    {
        Assert.IsEmpty(PlayCardAction.GetLegalActions(gameState, Card));
        Assert.Throws<ArgumentException>(() => new PlayCardAction(gameState, Card));
    }
}