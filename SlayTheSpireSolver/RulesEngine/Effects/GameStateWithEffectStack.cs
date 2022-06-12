namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GameStateWithEffectStack
{
    public GameState GameState { get; init; }
    public EffectStack EffectStack { get; init; } = new();

    public GameStateWithEffectStack(GameState gameState)
    {
        GameState = gameState;
    }

    public GameStateWithEffectStack(GameState gameState, EffectStack effectStack)
    {
        GameState = gameState;
        EffectStack = effectStack;
    }
}