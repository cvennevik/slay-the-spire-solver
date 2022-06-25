namespace SlayTheSpireSolver.RulesEngine.Effects;

public record UnresolvedGameState
{
    public GameState GameState { get; }
    public EffectStack EffectStack { get; } = new();

    public static implicit operator UnresolvedGameState(GameState gameState) => new(gameState);

    public UnresolvedGameState(GameState gameState)
    {
        GameState = gameState;
    }

    public UnresolvedGameState(GameState gameState, EffectStack effectStack)
    {
        GameState = gameState;
        EffectStack = effectStack;
    }

    public UnresolvedGameState(GameState gameState, params IEffect[] effects)
        : this(gameState, new EffectStack(effects)) { }

    public IReadOnlyList<GameState> Resolve()
    {
        return ResolveEffects(new UnresolvedGameState(GameState, EffectStack));
    }

    private IReadOnlyList<GameState> ResolveEffects(UnresolvedGameState unresolvedGameState)
    {
        if (unresolvedGameState.EffectStack.IsEmpty())
        {
            return new[] { unresolvedGameState.GameState };
        }

        return ResolveTopEffect(unresolvedGameState).SelectMany(ResolveEffects).Distinct().ToList();
    }

    private IReadOnlyList<UnresolvedGameState> ResolveTopEffect(GameState gameState, EffectStack effectStack)
    {
        var (effect, remainingEffectStack) = effectStack.Pop();
        var outcomes = effect.Resolve(gameState);
        return outcomes.Select(gameStateWithAddedEffects =>
            gameStateWithAddedEffects.GameState.WithEffectStack(
                remainingEffectStack.Push(gameStateWithAddedEffects.EffectStack))).ToList();
    }

    private IReadOnlyList<UnresolvedGameState> ResolveTopEffect(UnresolvedGameState unresolvedGameState)
    {
        return ResolveTopEffect(unresolvedGameState.GameState, unresolvedGameState.EffectStack);
    }
}