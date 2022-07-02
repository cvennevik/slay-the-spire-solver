namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ApplyToAllEnemiesEffect<T> : IEffect where T : TargetEnemyEffect, new()
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var resolveEnemyMoveEffects =
            gameState.EnemyParty.Select(enemy => new ResolveEnemyMoveEffect(enemy.Id)).Reverse();
        return gameState.WithEffects(new EffectStack(resolveEnemyMoveEffects));
    }
}