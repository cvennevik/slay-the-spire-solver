using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvableGameStatePossibility(ResolvableGameState ResolvableGameState, Probability Probability)
{
    public static implicit operator ResolvableGameStatePossibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
}