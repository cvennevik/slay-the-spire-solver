using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public class CommonCardTests<TCard> where TCard : Card, new()
{
    protected readonly TCard Card;
    protected readonly GameState BasicGameState;

    protected CommonCardTests()
    {
        Card = new TCard();
        BasicGameState = new GameState
        {
            PlayerHealth = 70,
            Energy = 3,
            EnemyParty = new EnemyParty(new JawWorm { Health = 40, IntendedMove = new Chomp() }),
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
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { PlayerHealth = 0 }));
    }

    [Test]
    public void NoLegalActionsWhenNoEnergy()
    {
        Assert.IsEmpty(Card.GetLegalActions(BasicGameState with { Energy = 0 }));
    }

    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new Action(BasicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetEffect(BasicGameState),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }
}