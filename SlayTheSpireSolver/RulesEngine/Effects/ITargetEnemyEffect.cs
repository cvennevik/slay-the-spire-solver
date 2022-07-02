using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect : IEffect
{
    protected TargetEnemyEffect(EnemyId Target)
    {
        this.Target = Target;
    }
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);
    public EnemyId Target { get; init; }
}