using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayCardAction(GameState GameState, Card Card, EffectStack CardEffects) : PlayerAction
{
    public PossibilitySet Resolve()
    {
        var unresolvedGameState = GameState with
        {
            EffectStack = new EffectStack(new AddCardToDiscardPileEffect(Card))
                .Push(CardEffects)
                .Push(new RemoveCardFromHandEffect(Card))
                .Push(new RemoveEnergyEffect(Card.GetCost()))
        };
        return unresolvedGameState.Resolve();
    }
}