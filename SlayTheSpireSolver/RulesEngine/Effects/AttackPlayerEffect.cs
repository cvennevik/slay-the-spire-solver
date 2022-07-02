using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackPlayerEffect(EnemyId EnemyId, Damage BaseDamage) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(EnemyId)) return gameState;
        var enemyStrength = gameState.EnemyParty.Get(EnemyId).Strength;
        var damagePlayerEffect = new DamagePlayerEffect(BaseDamage + enemyStrength);

        return gameState.WithEffects(damagePlayerEffect);
    }
}