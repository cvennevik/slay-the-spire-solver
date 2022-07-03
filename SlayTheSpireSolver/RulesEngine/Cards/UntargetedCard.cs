using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record UntargetedCard : Card
{
    public override IReadOnlyCollection<Action> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new[] { new Action(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(this),
                GetEffect(gameState),
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
        var expectedAction = new Action(_basicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(_card),
            _card.GetEffect(_basicGameState),
            new RemoveCardFromHandEffect(_card),
            new RemoveEnergyEffect(_card.GetCost())));
        Assert.AreEqual(expectedAction, _card.GetLegalActions(_basicGameState).Single());
    }
}