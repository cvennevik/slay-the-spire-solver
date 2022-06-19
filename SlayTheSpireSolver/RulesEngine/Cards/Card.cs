using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public abstract Energy GetCost();
    public abstract IEffect GetEffect(GameState gameState);

    public IReadOnlyCollection<IAction> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new IAction[] { new ActionWithEffectStack(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(this),
                GetEffect(gameState),
                new RemoveCardFromHandEffect(this),
                new RemoveEnergyEffect(GetCost()))) }
            : Array.Empty<IAction>();
    }

    private bool CanBePlayed(GameState gameState)
    {
        return !gameState.IsCombatOver()
               && gameState.Hand.Contains(this)
               && gameState.Energy >= GetCost();
    }
}
