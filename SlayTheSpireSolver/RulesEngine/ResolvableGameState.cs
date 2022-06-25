using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvableGameState
{
    public GameState GameState { get; }
    public EffectStack EffectStack { get; } = new();

    public static implicit operator ResolvableGameState(GameState gameState) => new(gameState);

    public ResolvableGameState(GameState gameState, params IEffect[] effects)
        : this(gameState, new EffectStack(effects)) { }

    public ResolvableGameState(GameState gameState, EffectStack effectStack)
    {
        GameState = gameState;
        EffectStack = effectStack;
    }

    public PossibilitySet Resolve()
    {
        return new PossibilitySet(
            ResolveEffects(new ResolvableGameState(GameState, EffectStack))
                .Select(x => (Possibility) x)
                .ToArray());
    }

    private IReadOnlyList<GameState> ResolveEffects(ResolvableGameState resolvableGameState)
    {
        if (resolvableGameState.EffectStack.IsEmpty())
        {
            return new[] { resolvableGameState.GameState };
        }

        return ResolveTopEffect(resolvableGameState).SelectMany(ResolveEffects).Distinct().ToList();
    }

    private IReadOnlyList<ResolvableGameState> ResolveTopEffect(GameState gameState, EffectStack effectStack)
    {
        var (effect, remainingEffectStack) = effectStack.Pop();
        var outcomes = effect.Resolve(gameState);
        return outcomes.Select(gameStateWithAddedEffects =>
            gameStateWithAddedEffects.ResolvableGameState.GameState.WithEffects(
                remainingEffectStack.Push(gameStateWithAddedEffects.ResolvableGameState.EffectStack))).ToList();
    }

    private IReadOnlyList<ResolvableGameState> ResolveTopEffect(ResolvableGameState resolvableGameState)
    {
        return ResolveTopEffect(resolvableGameState.GameState, resolvableGameState.EffectStack);
    }

    public Possibility WithProbability(Probability probability) => new(this, probability);
}