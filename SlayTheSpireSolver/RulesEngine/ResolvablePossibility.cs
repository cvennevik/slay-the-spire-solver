using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvablePossibility(ResolvableGameState ResolvableGameState, Probability Probability)
{
    public static implicit operator ResolvablePossibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
    public static implicit operator ResolvablePossibility(GameState gameState) =>
        new(gameState, new Probability(1));

    private IReadOnlyList<GameStatePossibility> Resolve()
    {
        if (ResolvableGameState.EffectStack.IsEmpty())
        {
            return new GameStatePossibility[] { ResolvableGameState.GameState };
        }

        return new GameStatePossibility[] { ResolvableGameState.GameState };
    }

    public ResolvablePossibility WithBaseEffectStack(EffectStack baseEffectStack)
    {
        return this with
        {
            ResolvableGameState = ResolvableGameState.WithBaseEffectStack(baseEffectStack)
        };
    }    
}