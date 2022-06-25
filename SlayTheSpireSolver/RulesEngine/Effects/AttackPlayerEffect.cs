using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackPlayerEffect(EnemyId EnemyId, Damage BaseDamage) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState;
    }
}