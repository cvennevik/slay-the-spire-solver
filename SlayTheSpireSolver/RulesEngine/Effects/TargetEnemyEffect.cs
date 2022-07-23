using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect : Effect
{
    public EnemyId Target { get; init; } = EnemyId.Default;

    protected TargetEnemyEffect()
    {
    }

    protected TargetEnemyEffect(EnemyId target)
    {
        Target = target;
    }

    public abstract PossibilitySet Resolve(GameState gameState);
}