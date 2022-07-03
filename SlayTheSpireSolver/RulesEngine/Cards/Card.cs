using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public abstract Energy GetCost();
    public abstract Effect GetEffect(GameState gameState);

    public virtual IReadOnlyCollection<Action> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new[] { new Action(gameState, new EffectStack(
                new AddCardToDiscardPileEffect(this),
                GetEffect(gameState),
                new RemoveCardFromHandEffect(this),
                new RemoveEnergyEffect(GetCost()))) }
            : Array.Empty<Action>();
    }

    private bool CanBePlayed(GameState gameState)
    {
        return !gameState.IsCombatOver()
               && gameState.Hand.Contains(this)
               && gameState.Energy >= GetCost();
    }
}
