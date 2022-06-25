using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect
{
    private readonly EnemyId _enemyId;

    public ResolveEnemyMoveEffect(EnemyId enemyId)
    {
        _enemyId = enemyId;
    }
}