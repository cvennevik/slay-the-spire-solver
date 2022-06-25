namespace SlayTheSpireSolver.RulesEngine.Effects;

public class ResolveAllEnemyMovesEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var resolveEnemyMoveEffects =
            gameState.EnemyParty.Select(enemy => (IEffect)new ResolveEnemyMoveEffect(enemy.Id)).Reverse();
        return new[] { gameState.WithEffectStack(new EffectStack(resolveEnemyMoveEffects.ToArray())) };
    }
}