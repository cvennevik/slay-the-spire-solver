using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public abstract Energy GetCost();
    public abstract Effect GetEffect(GameState gameState);
    public abstract IReadOnlyCollection<Action> GetLegalActions(GameState gameState);
}
