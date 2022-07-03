using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public abstract Energy GetCost();
    public abstract Effect GetEffect(GameState gameState);
    public abstract IReadOnlyCollection<Action> GetLegalActions(GameState gameState);

    protected bool CanBePlayed(GameState gameState)
    {
        return !gameState.IsCombatOver()
               && gameState.Hand.Contains(this)
               && gameState.Energy >= GetCost();
    }
}

internal class CommonCardTests<TCard> where TCard : Card, new()
{
    private readonly TCard _card;
    private readonly GameState _basicGameState;

    protected CommonCardTests()
    {
        _card = new TCard();
        _basicGameState = new GameState
        {
            PlayerHealth = 70,
            Energy = 3,
            EnemyParty = new EnemyParty(new JawWorm { Health = 40, IntendedMove = new Chomp() }),
            Hand = new Hand(_card),
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
        Assert.IsEmpty(_card.GetLegalActions(_basicGameState with { EnemyParty = new EnemyParty() }));
    }

    [Test]
    public void NoLegalActionsWhenCardNotInHand()
    {
        Assert.IsEmpty(_card.GetLegalActions(_basicGameState with { Hand = new Hand() }));
    }

    [Test]
    public void NoLegalActionsWhenPlayerDefeated()
    {
        Assert.IsEmpty(_card.GetLegalActions(_basicGameState with { PlayerHealth = 0 }));
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        Assert.IsEmpty(_card.GetLegalActions(_basicGameState with { Energy = 0 }));
    }

    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new Action(_basicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(_card),
            _card.GetEffect(_basicGameState),
            new RemoveCardFromHandEffect(_card),
            new RemoveEnergyEffect(_card.GetCost())));
        Assert.AreEqual(expectedAction, _card.GetLegalActions(_basicGameState).Single());
    }
}