namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct NullEffect : IEffect
{
    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        return new[] { gameState };
    }

    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        return new[] { new GameStateWithEffectStack(gameState) };
    }
}