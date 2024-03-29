﻿using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card : IComparable<Card>
{
    public abstract Energy Cost { get; }
    protected abstract string Name { get; }
    public virtual bool IsEthereal => false;
    public abstract IReadOnlyCollection<PlayCardAction> GetLegalActions(GameState gameState);

    protected bool CanBePlayed(GameState gameState)
    {
        return !gameState.CombatHasEnded
               && gameState.Hand.Contains(this)
               && gameState.Energy >= Cost;
    }

    public int CompareTo(Card? other)
    {
        if (other == null) return -1;
        return string.Compare(Name, other.Name, StringComparison.Ordinal);
    }

    public sealed override string ToString()
    {
        return Name;
    }
}

internal abstract class CardTests<TCard> where TCard : Card, new()
{
    protected readonly GameState BasicGameState;
    protected readonly TCard Card;

    protected CardTests()
    {
        Card = new TCard();
        BasicGameState = new GameState
        {
            Energy = 3,
            EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() }),
            Hand = new Hand(Card),
            DiscardPile = new DiscardPile()
        };
    }

    [Test]
    public void InstancesAreEqual()
    {
        Assert.AreEqual(new TCard(), new TCard());
    }

    [Test]
    public void NoLegalActionsWhenCombatHasEnded()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { CombatHasEnded = true }));
    }

    [Test]
    public void NoLegalActionsWhenCardNotInHand()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { Hand = new Hand() }));
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { Energy = 0 }));
    }
}