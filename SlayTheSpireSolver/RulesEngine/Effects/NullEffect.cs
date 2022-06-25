namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct NullEffect : IEffect
{
    public IReadOnlyCollection<UnresolvedGameState> Resolve(GameState gameState)
    {
        return new[] { new UnresolvedGameState(gameState) };
    }
}