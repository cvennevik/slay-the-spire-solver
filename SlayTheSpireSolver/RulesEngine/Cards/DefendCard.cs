using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record DefendCard : ICard
{
    private static readonly Energy Cost = new(1);
    private static readonly IEffect Effect = new GainPlayerArmorEffect(5);

    public Energy GetCost() => Cost;
    public IEffect GetEffect(GameState gameState) => Effect;

    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        return PlayCardAction.IsLegal(gameState, this)
            ? new IAction[] { new PlayCardAction(gameState, this) }
            : Array.Empty<IAction>();
    }
}
