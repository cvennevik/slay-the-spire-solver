using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyStrengthEffect(EnemyId EnemyId, Strength StrengthGain) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        if (gameState.EnemyParty.Has(EnemyId))
        {
            var newEnemyParty = gameState.EnemyParty.ModifyEnemy(EnemyId,
                enemy => enemy with { Strength = StrengthGain });
            return gameState with { EnemyParty = newEnemyParty };
        }
        return gameState;
    }
}