using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record TargetedCard : Card
{
    public abstract Effect GetEffect(EnemyId target);

    public override IReadOnlyCollection<Action> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new[] { new Action(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(this),
                GetEffect(gameState.EnemyParty.First().Id),
                new RemoveCardFromHandEffect(this),
                new RemoveEnergyEffect(GetCost()))) }
            : Array.Empty<Action>();
    }
}

internal class TargetedCardTests<TCard> : CommonCardTests<TCard> where TCard : TargetedCard, new()
{
    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new Action(BasicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetEffect(BasicGameState.EnemyParty.First().Id),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }
}