using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GainEnemyArmorEffect(EnemyId EnemyId, Armor Armor) : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var enemyParty = gameState.EnemyParty.ModifyEnemy(EnemyId, enemy => enemy with { Armor = Armor });
        var result = gameState with { EnemyParty = enemyParty };
        return new[] { result.WithEffectStack() };
    }
}