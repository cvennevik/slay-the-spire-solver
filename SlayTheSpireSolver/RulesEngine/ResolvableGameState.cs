using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvableGameState
{
    public GameState GameState { get; }
    public EffectStack EffectStack { get; }

    public ResolvableGameState(GameState gameState)
    {
        GameState = gameState with { EffectStack = new EffectStack() };
        EffectStack = gameState.EffectStack;
    }

    public ResolvablePossibility WithProbability(Probability probability) => new(GameState, probability);
}