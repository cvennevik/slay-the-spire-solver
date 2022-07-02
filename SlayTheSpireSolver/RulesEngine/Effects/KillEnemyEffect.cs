using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect(EnemyId _targetId) : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState with { EnemyParty = gameState.EnemyParty.Remove(_targetId) };
    }
}