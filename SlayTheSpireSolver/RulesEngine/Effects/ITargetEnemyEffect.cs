using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect : IEffect
{
    public EnemyId Target { get; init; } = EnemyId.Default;

    public TargetEnemyEffect()
    {
        
    }
    
    public TargetEnemyEffect(EnemyId target)
    {
        Target = target;
    }

    public abstract ResolvablePossibilitySet Resolve(GameState gameState);
}