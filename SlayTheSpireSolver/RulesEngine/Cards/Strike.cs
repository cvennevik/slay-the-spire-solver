using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Strike : Card
{
    private static readonly Energy Cost = new(1);

    public override Energy GetCost() => Cost;
    public override IEffect GetEffect(GameState gameState) => new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(6));

    public override IReadOnlyCollection<IAction> GetLegalActions(GameState gameState) =>
        PlayCardAction.GetLegalActions(gameState, this);
}
