using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect(EnemyId Target) : IEffect
{
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);
}