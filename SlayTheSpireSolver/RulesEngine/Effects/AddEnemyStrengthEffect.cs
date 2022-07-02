using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyStrengthEffect(EnemyId EnemyId, Strength StrengthGain) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState.ModifyEnemy(EnemyId,
            enemy => enemy with { Strength = enemy.Strength + StrengthGain });
    }
}