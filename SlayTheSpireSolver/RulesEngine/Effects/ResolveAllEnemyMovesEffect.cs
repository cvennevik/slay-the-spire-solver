namespace SlayTheSpireSolver.RulesEngine.Effects;

public class ResolveAllEnemyMovesEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var resolveEnemyMoveEffects = new List<IEffect>();
        foreach (var enemy in gameState.EnemyParty)
        {
            resolveEnemyMoveEffects.Add(new ResolveEnemyMoveEffect(enemy.Id));
        }
        return new[] { gameState.WithEffectStack(new EffectStack(resolveEnemyMoveEffects.ToArray())) };
    }
}