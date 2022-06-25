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

    public IReadOnlyList<GameStatePossibility> Resolve()
    {
        return ResolveEffects();
    }

    private IReadOnlyList<GameStatePossibility> ResolveEffects()
    {
        if (EffectStack.IsEmpty())
        {
            return new GameStatePossibility[] { GameState };
        }

        return ResolveTopEffect().SelectMany(x => x.ResolveEffects()).Distinct().ToList();
    }

    private IReadOnlyList<ResolvableGameState> ResolveTopEffect()
    {
        var (effect, remainingEffectStack) = EffectStack.Pop();
        var resolvablePossibilitySet = effect.Resolve(GameState);
        return resolvablePossibilitySet.Select(resolvablePossibility =>
            resolvablePossibility.ResolvableGameState.GameState.WithEffects(
                remainingEffectStack.Push(resolvablePossibility.ResolvableGameState.EffectStack)))
            .ToList();
    }

    public ResolvablePossibility WithProbability(Probability probability) => new(this, probability);
}