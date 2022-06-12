using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Strike : Card
{
    public Strike()
    {
        Cost = new Energy(1);
    }

    public override IEffect GetEffect(GameState gameState) => new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(6));

    public override IReadOnlyCollection<IAction> GetLegalActions(GameState gameState) =>
        PlayCardAction.GetLegalActions(gameState, this);
}
