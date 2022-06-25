namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct NullEffect : IEffect
{
    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        return new[] { new ResolvableGameState(gameState) };
    }
}