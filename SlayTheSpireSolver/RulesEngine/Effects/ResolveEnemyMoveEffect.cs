using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect : IEffect
{
    private readonly EnemyId _enemyId;

    public ResolveEnemyMoveEffect(EnemyId enemyId)
    {
        _enemyId = enemyId;
    }

    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        if (gameState.EnemyParty.Has(_enemyId))
        {
            var enemy = gameState.EnemyParty.Get(_enemyId);
            var moveEffects = enemy.GetMoveEffects();
            return new[] { gameState.WithEffectStack(moveEffects) };
        }

        return new[] { gameState.WithEffectStack() };
    }
}