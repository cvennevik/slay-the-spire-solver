using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect
{
    private EnemyId Id { get; }

    public ResolveEnemyMoveEffect(EnemyId enemyId)
    {
        Id = enemyId;
    }
}