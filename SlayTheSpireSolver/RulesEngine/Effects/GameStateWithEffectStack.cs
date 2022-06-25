namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GameStateWithEffectStack
{
    public GameState GameState { get; }
    public EffectStack EffectStack { get; } = new();

    public static implicit operator GameStateWithEffectStack(GameState gameState) =>
        new GameStateWithEffectStack(gameState);

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