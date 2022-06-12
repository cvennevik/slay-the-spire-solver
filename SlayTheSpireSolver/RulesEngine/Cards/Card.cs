using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record Card
{
    public Energy Cost { get; init; }
    public IEffect Effect { get; init; }

    public virtual Energy GetCost() => Cost;
    public virtual IEffect GetEffect(GameState gameState) => Effect;
    public abstract IReadOnlyCollection<IAction> GetLegalActions(GameState gameState);
}
