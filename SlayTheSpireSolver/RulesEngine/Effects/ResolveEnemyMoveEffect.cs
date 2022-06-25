using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect
{
    private readonly EnemyId Id;

    public ResolveEnemyMoveEffect(EnemyId enemyId)
    {
        Id = enemyId;
    }
}