using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyStrengthEffect(EnemyId EnemyId, Strength StrengthGain) : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        return gameState.ModifyEnemy(EnemyId,
            enemy => enemy with { Strength = enemy.Strength + StrengthGain });
    }
}