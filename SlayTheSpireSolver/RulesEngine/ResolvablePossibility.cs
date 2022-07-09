using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvablePossibility
{
    public ResolvableGameState ResolvableGameState { get; }
    public GameState GameState { get; }
    public Probability Probability { get; init; }

    public ResolvablePossibility(ResolvableGameState ResolvableGameState, Probability Probability)
    {
        this.ResolvableGameState = ResolvableGameState;
        this.GameState = ResolvableGameState.GameState;
        this.Probability = Probability;
    }

    public static implicit operator ResolvablePossibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
    public static implicit operator ResolvablePossibility(GameState gameState) =>
        new(gameState.WithEffects(), new Probability(1));
    public static implicit operator ResolvablePossibility(Possibility possibility) =>
        new(possibility.GameState.WithEffects(), possibility.Probability);

    private ResolvablePossibilitySet ResolveTopEffect()
    {
        var (effect, remainingEffectStack) = ResolvableGameState.EffectStack.Pop();
        return effect
            .ResolveWithBaseEffectStack(GameState, remainingEffectStack)
            .Select(resolvablePossibility => resolvablePossibility with {Probability = resolvablePossibility.Probability * Probability})
            .ToArray();
    }

    public ResolvablePossibility WithBaseEffectStack(EffectStack baseEffectStack)
    {
        return new ResolvablePossibility(GameState.WithEffects(baseEffectStack.Push(ResolvableGameState.EffectStack)),
            Probability);
    }
}