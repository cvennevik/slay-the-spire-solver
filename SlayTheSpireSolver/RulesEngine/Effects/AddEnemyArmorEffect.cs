using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyArmorEffect(EnemyId EnemyId, Armor ArmorGain) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        var enemyParty = gameState.EnemyParty.ModifyEnemy(EnemyId,
            enemy => enemy with { Armor = enemy.Armor + ArmorGain });
        return gameState with { EnemyParty = enemyParty };
    }
}