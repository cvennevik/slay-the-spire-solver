using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record UntargetedCard : Card
{
    public abstract Effect GetEffect();

    public override IReadOnlyCollection<PlayerAction> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new[] { new PlayUntargetedCardAction(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(this),
                GetEffect(),
                new RemoveCardFromHandEffect(this),
                new RemoveEnergyEffect(GetCost()))) }
            : Array.Empty<PlayerAction>();
    }
}

internal abstract class UntargetedCardTests<TCard> : CardTests<TCard> where TCard : UntargetedCard, new()
{
    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new PlayUntargetedCardAction(BasicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetEffect(),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }
}