using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayCardAction : PlayerAction
{
    protected PlayCardAction(GameState gameState, Card card, EffectStack cardEffects)
        : base(gameState, new EffectStack(new AddCardToDiscardPileEffect(card))
            .Push(cardEffects)
            .Push(new RemoveCardFromHandEffect(card))
            .Push(new RemoveEnergyEffect(card.GetCost()))) { }
}