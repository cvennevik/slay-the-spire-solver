using SlayTheSpireSolver.RulesEngine.Effects;

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
}