using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

public class CommonCardTests<TCard> where TCard : Card, new()
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
    public void NoLegalActionsWhenNoEnemies()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { EnemyParty = new EnemyParty() }));
    }

    [Test]
    public void NoLegalActionsWhenCardNotInHand()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { Hand = new Hand() }));
    }

    [Test]
    public void NoLegalActionsWhenPlayerDefeated()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { PlayerHealth = new Health(0) }));
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { Energy = new Energy(0) }));
    }

    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new ActionWithEffectStack(BasicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetEffect(BasicGameState),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }
}