using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card : IComparable<Card>
{
    public abstract Energy Cost { get; }
    public abstract string Name { get; }
    public abstract IReadOnlyCollection<PlayCardAction> GetLegalActions(GameState gameState);
    protected abstract string GetName();

    protected bool CanBePlayed(GameState gameState)
    {
        return !gameState.IsCombatOver()
               && gameState.Hand.Contains(this)
               && gameState.Energy >= Cost;
    }

    public int CompareTo(Card? other)
    {
        if (other == null) return -1;
        return string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
    }

    public sealed override string ToString()
    {
        return GetName();
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
            PlayerHealth = 70,
            Energy = 3,
            EnemyParty = new EnemyParty(new JawWorm { Health = 40, IntendedMove = new Chomp() }),
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
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { PlayerHealth = 0 }));
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { Energy = 0 }));
    }
}