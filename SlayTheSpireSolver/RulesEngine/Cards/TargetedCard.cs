using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record TargetedCard : Card
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