namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GameStateWithEffectStack
{
    public GameState GameState { get; init; }
    public IReadOnlyList<IEffect> UnresolvedEffects { get; init; } = Array.Empty<IEffect>();

    public GameStateWithEffectStack(GameState gameState)
    {
        GameState = gameState;
    }
}