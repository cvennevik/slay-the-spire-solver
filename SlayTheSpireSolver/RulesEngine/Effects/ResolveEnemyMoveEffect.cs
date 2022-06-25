using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect : IEffect
{
    private readonly EnemyId _enemyId;

    public ResolveEnemyMoveEffect(EnemyId enemyId)
    {
        _enemyId = enemyId;
    }

    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(_enemyId)) return gameState;
        var moveEffects = gameState.EnemyParty.Get(_enemyId).GetMoveEffects();
        return gameState.WithEffects(moveEffects);
    }
}