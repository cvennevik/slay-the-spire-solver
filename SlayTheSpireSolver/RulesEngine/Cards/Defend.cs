using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Defend : ICard
{
    private static readonly Energy Cost = new(1);
    private static readonly IEffect Effect = new GainPlayerArmorEffect(new Armor(5));

    public Energy GetCost() => Cost;
    public IEffect GetEffect(GameState gameState) => Effect;

    public IReadOnlyCollection<IAction> GetLegalActions(GameState gameState) =>
        PlayCardAction.GetLegalActions(gameState, this);
}
