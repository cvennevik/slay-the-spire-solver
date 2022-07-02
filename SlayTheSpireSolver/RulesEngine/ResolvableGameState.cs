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

    public IReadOnlyList<Possibility> Resolve()
    {
        return WithProbability(1).Resolve();
    }

    public ResolvableGameState WithBaseEffectStack(EffectStack baseEffectStack)
    {
        return GameState.WithEffects(baseEffectStack.Push(EffectStack));
    }

    public ResolvablePossibility WithProbability(Probability probability) => new(this, probability);
}