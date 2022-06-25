namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveAllEnemyMovesEffect : IEffect
{
    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        var resolveEnemyMoveEffects =
            gameState.EnemyParty.Select(enemy => (IEffect)new ResolveEnemyMoveEffect(enemy.Id)).Reverse();
        return new[] { gameState.WithEffectStack(new EffectStack(resolveEnemyMoveEffects.ToArray())) };
    }
}