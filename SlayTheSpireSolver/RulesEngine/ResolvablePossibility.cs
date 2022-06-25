using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvablePossibility(ResolvableGameState ResolvableGameState, Probability Probability)
{
    public static implicit operator ResolvablePossibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
    public static implicit operator ResolvablePossibility(GameState gameState) =>
        new(gameState, new Probability(1));

    public ResolvablePossibility WithBaseEffectStack(EffectStack baseEffectStack)
    {
        return this;
    }
}