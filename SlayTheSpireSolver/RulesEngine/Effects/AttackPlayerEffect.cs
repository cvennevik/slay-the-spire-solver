using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackPlayerEffect(EnemyId EnemyId, Damage BaseDamage) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState.EnemyParty.Has(EnemyId)
            ? gameState.WithEffects(new EffectStack(new DamagePlayerEffect(BaseDamage.Amount + gameState.EnemyParty.Get(EnemyId).Strength.Amount)))
            : gameState;
    }
}