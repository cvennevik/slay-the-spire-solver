using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public abstract Energy GetCost();
    public abstract IEffect GetEffect(GameState gameState);
    public abstract IReadOnlyCollection<IAction> GetLegalActions(GameState gameState);
}
