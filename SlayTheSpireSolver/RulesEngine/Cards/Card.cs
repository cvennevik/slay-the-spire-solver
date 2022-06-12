using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public abstract Energy GetCost();
    public abstract IEffect GetEffect(GameState gameState);

    public IReadOnlyCollection<IAction> GetLegalActions(GameState gameState)
    {
        return PlayCardAction.IsLegal(gameState, this)
            ? new IAction[] { new PlayCardAction(gameState, this) }
            : Array.Empty<IAction>();
    }

}
