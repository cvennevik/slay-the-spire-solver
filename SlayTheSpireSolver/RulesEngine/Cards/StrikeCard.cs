using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record StrikeCard : ICard
{
    private static readonly Energy Cost = new(1);

    public Energy GetCost() => Cost;
    public IEffect GetEffect(GameState gameState) => new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(6));
}
