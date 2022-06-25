using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect : IEffect
{
    private readonly EnemyId _enemyId;

    public ResolveEnemyMoveEffect(EnemyId enemyId)
    {
        _enemyId = enemyId;
    }

    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(_enemyId)) return gameState;
        var enemyMoveEffects = gameState.EnemyParty.Get(_enemyId).GetMoveEffects();
        return gameState.WithEffects(enemyMoveEffects);
    }
}