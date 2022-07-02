using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect : Effect
{
    public EnemyId Target { get; init; } = EnemyId.Default;

    public TargetEnemyEffect() { }

    public TargetEnemyEffect(EnemyId target)
    {
        Target = target;
    }
}