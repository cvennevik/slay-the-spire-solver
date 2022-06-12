using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Defend : Card
{
    private static readonly Energy Cost = new(1);
    private static readonly IEffect Effect = new GainPlayerArmorEffect(new Armor(5));

    public override Energy GetCost() => Cost;
    public override IEffect GetEffect(GameState gameState) => Effect;

    public override IReadOnlyCollection<IAction> GetLegalActions(GameState gameState) =>
        PlayCardAction.GetLegalActions(gameState, this);
}
