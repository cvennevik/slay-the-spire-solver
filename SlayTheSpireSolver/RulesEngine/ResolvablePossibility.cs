using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvablePossibility(ResolvableGameState ResolvableGameState, Probability Probability)
{
    public static implicit operator ResolvablePossibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
    public static implicit operator ResolvablePossibility(GameState gameState) =>
        new(gameState, new Probability(1));

    public IReadOnlyList<Possibility> Resolve()
    {
        if (ResolvableGameState.EffectStack.IsEmpty())
        {
            return new[] { ResolvableGameState.GameState.WithProbability(Probability) };
        }

        return ResolveTopEffect()
            .SelectMany(x => x.Resolve())
            .GroupBy(x => x.GameState)
            .Select(grouping => new Possibility(grouping.Key, grouping.Select(x => x.Probability).Aggregate((acc, x) => acc.Add(x))))
            .ToList();
    }

    public ResolvablePossibilitySet ResolveTopEffect()
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
            ResolvableGameState = ResolvableGameState.WithBaseEffectStack(baseEffectStack)
        };
    }    
}