namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveAllEnemyMovesEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        var resolveEnemyMoveEffects =
            gameState.EnemyParty.Select(enemy => new ResolveEnemyMoveEffect(enemy.Id)).Reverse();
        return gameState.WithEffects(new EffectStack(resolveEnemyMoveEffects));
    }
}