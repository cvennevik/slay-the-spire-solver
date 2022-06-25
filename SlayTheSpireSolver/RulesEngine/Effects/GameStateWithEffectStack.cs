namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GameStateWithEffectStack
{
    public GameState GameState { get; }
    public EffectStack EffectStack { get; } = new();

    public static implicit operator GameStateWithEffectStack(GameState gameState) => new(gameState);

    public GameStateWithEffectStack(GameState gameState)
    {
        GameState = gameState;
    }

    public GameStateWithEffectStack(GameState gameState, EffectStack effectStack)
    {
        GameState = gameState;
        EffectStack = effectStack;
    }

    public GameStateWithEffectStack(GameState gameState, params IEffect[] effects)
        : this(gameState, new EffectStack(effects)) { }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        return ResolveEffects(new GameStateWithEffectStack(GameState, EffectStack));
    }

    protected IReadOnlyList<GameState> ResolveEffects(GameStateWithEffectStack gameStateWithEffectStack)
    {
        if (gameStateWithEffectStack.EffectStack.IsEmpty())
        {
            return new[] { gameStateWithEffectStack.GameState };
        }

        return ResolveTopEffect(gameStateWithEffectStack).SelectMany(ResolveEffects).Distinct().ToList();
    }

    private IReadOnlyList<GameStateWithEffectStack> ResolveTopEffect(GameState gameState, EffectStack effectStack)
    {
        var (effect, remainingEffectStack) = effectStack.Pop();
        var outcomes = effect.Resolve(gameState);
        return outcomes.Select(gameStateWithAddedEffects =>
            gameStateWithAddedEffects.GameState.WithEffectStack(
                remainingEffectStack.Push(gameStateWithAddedEffects.EffectStack))).ToList();
    }

    private IReadOnlyList<GameStateWithEffectStack> ResolveTopEffect(GameStateWithEffectStack gameStateWithEffectStack)
    {
        return ResolveTopEffect(gameStateWithEffectStack.GameState, gameStateWithEffectStack.EffectStack);
    }
}