using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Defend : Card
{
    public Defend()
    {
        Cost = new Energy(1);
    }

    private static readonly IEffect Effect = new GainPlayerArmorEffect(new Armor(5));

    public override IEffect GetEffect(GameState gameState) => Effect;

    public override IReadOnlyCollection<IAction> GetLegalActions(GameState gameState) =>
        PlayCardAction.GetLegalActions(gameState, this);
}
