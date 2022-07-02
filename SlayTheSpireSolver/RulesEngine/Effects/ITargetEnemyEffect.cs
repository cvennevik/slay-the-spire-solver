using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect : IEffect
{
    public EnemyId Target { get; init; }

    public TargetEnemyEffect(EnemyId Target)
    {
        this.Target = Target;
    }

    public abstract ResolvablePossibilitySet Resolve(GameState gameState);
}