namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GameStateWithUnresolvedEffects
{
    public GameState GameState { get; init; }
    public IReadOnlyList<IEffect> UnresolvedEffects { get; init; } = Array.Empty<IEffect>();

    public GameStateWithUnresolvedEffects(GameState gameState)
    {
        GameState = gameState;
    }
}