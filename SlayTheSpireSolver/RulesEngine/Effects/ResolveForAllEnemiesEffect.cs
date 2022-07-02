namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveForAllEnemiesEffect<T> : IEffect where T : TargetEnemyEffect, new()
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var resolveEnemyMoveEffects =
            gameState.EnemyParty.Select(enemy => new T {Target = enemy.Id}).Reverse();
        return gameState.WithEffects(new EffectStack(resolveEnemyMoveEffects));
    }
}