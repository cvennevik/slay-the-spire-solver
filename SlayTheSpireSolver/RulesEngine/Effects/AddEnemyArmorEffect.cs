using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyArmorEffect(EnemyId EnemyId, Armor ArmorGain) : IEffect
{
    public PossibilitySet Resolve(GameState gameState)
    {
        return gameState.ModifyEnemy(EnemyId,
            enemy => enemy with { Armor = enemy.Armor + ArmorGain });
    }
}