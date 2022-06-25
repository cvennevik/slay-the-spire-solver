namespace SlayTheSpireSolver.RulesEngine.Effects;

public class ResolveAllEnemyMovesEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        return new[] { gameState.WithEffectStack() };
    }
}