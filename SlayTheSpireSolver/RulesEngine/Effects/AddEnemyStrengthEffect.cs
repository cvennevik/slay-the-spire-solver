using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyStrengthEffect(EnemyId EnemyId, Strength StrengthGain) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        if (gameState.EnemyParty.Has(EnemyId))
        {
            return gameState;
        }
        return gameState;
    }
}