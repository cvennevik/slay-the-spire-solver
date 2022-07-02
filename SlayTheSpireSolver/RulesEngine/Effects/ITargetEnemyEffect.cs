using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect : IEffect
{
    public EnemyId Target { get; init; }

    protected TargetEnemyEffect(EnemyId Target)
    {
        this.Target = Target;
    }

    public abstract ResolvablePossibilitySet Resolve(GameState gameState);
}