using NUnit.Framework;
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

    // GOAL: Add EffectStack to GameState, get rid of this class
    // PLAN:
    //  1. Add EffectStack to GameState
    //  2. Replace passing separate EffectStack to ResolvableGameState
    //  3a. Replace using ResolvableGameState.EffectStack with using GameState.EffectStack
    //  3b. Replace using ResolvableGameState with using GameState 

    public IReadOnlyList<Possibility> Resolve()
    {
        return WithProbability(1).Resolve();
    }

    public ResolvablePossibility WithProbability(Probability probability) => new(this, probability);
}

[TestFixture]
internal class ResolvableGameStateTests
{    

}