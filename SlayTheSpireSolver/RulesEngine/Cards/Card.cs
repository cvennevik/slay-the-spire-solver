using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public abstract Energy GetCost();
    public abstract IEffect GetEffect(GameState gameState);

    public IReadOnlyCollection<IAction> GetLegalActions(GameState gameState)
    {
        return IsLegal(gameState)
            ? new IAction[] { new PlayCardAction(gameState, this) }
            : Array.Empty<IAction>();
    }

    public bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver()
               && gameState.Hand.Contains(this)
               && gameState.Energy >= GetCost();
    }
}
