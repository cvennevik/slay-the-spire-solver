using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackPlayerEffect(EnemyId EnemyId, Damage BaseDamage) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(EnemyId)) return gameState;
        var enemyStrength = gameState.EnemyParty.Get(EnemyId).Strength;

        return gameState.WithEffects(new EffectStack(
            new DamagePlayerEffect(BaseDamage.Amount + enemyStrength.Amount)));
    }
}