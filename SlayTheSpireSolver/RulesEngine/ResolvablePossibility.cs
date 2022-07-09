using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvablePossibility
{
    public ResolvableGameState ResolvableGameState { get; init; }
    public GameState GameState { get; init; }
    public Probability Probability { get; init; }

    public ResolvablePossibility(ResolvableGameState ResolvableGameState, Probability Probability)
    {
        this.ResolvableGameState = ResolvableGameState;
        GameState = ResolvableGameState.GameState;
        this.Probability = Probability;
    }

    public static implicit operator ResolvablePossibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
    public static implicit operator ResolvablePossibility(GameState gameState) =>
        new(gameState.WithEffects(), new Probability(1));
    public static implicit operator ResolvablePossibility(Possibility possibility) =>
        new(possibility.GameState.WithEffects(), possibility.Probability);

    public IReadOnlyList<Possibility> Resolve()
    {
        if (ResolvableGameState.EffectStack.IsEmpty())
        {
            return new[] { ResolvableGameState.GameState.WithProbability(Probability) };
        }

        return ResolveTopEffect()
            .SelectMany(x => x.Resolve())
            .GroupBy(x => x.GameState)
            .Select(grouping => new Possibility(grouping.Key,
                grouping.Select(x => x.Probability).Aggregate((acc, x) => acc.Add(x))))
            .ToList();
    }

    private ResolvablePossibilitySet ResolveTopEffect()
    {
        var (effect, remainingEffectStack) = ResolvableGameState.EffectStack.Pop();
        return effect
            .ResolveWithBaseEffectStack(ResolvableGameState.GameState, remainingEffectStack)
            .Select(resolvablePossibility => resolvablePossibility with {Probability = resolvablePossibility.Probability * Probability})
            .ToArray();
    }

    public ResolvablePossibility WithBaseEffectStack(EffectStack baseEffectStack)
    {
        return this with
        {
            ResolvableGameState = ResolvableGameState.GameState.WithEffects(baseEffectStack.Push(ResolvableGameState.EffectStack))
        };
    }
}