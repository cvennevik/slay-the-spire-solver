using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GainEnemyArmorEffect(EnemyId EnemyId, Armor ArmorGain) : IEffect
{
    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        var enemyParty = gameState.EnemyParty.ModifyEnemy(EnemyId,
            enemy => enemy with { Armor = enemy.Armor + ArmorGain });
        var result = gameState with { EnemyParty = enemyParty };
        return new[] { result.WithEffectStack() };
    }
}