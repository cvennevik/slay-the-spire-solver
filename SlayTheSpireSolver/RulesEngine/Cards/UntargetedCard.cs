using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record UntargetedCard : Card
{
    public abstract Effect GetEffect();

    public override IReadOnlyCollection<Action> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new[] { new Action(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(this),
                GetEffect(),
                new RemoveCardFromHandEffect(this),
                new RemoveEnergyEffect(GetCost()))) }
            : Array.Empty<Action>();
    }
}

internal class UntargetedCardTests<TCard> : CommonCardTests<TCard> where TCard : UntargetedCard, new()
{
    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new Action(BasicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetEffect(),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }
}