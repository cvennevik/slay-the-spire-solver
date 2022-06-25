using System;
using System.Linq;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

public static class ResolvableGameStatePossibilitySetExtensions
{
    public static GameState SingleResolvedState(this ResolvableGameStatePossibilitySet resolvableGameStatePossibilitySet)
    {
        var unresolvedGameState = resolvableGameStatePossibilitySet.Single().ResolvableGameState;
        if (!unresolvedGameState.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Game state is not resolved; effect stack is not empty");
        }

        return unresolvedGameState.GameState;
    }

    public static ResolvableGameState SingleUnresolvedState(this ResolvableGameStatePossibilitySet resolvableGameStatePossibilitySet)
    {
        var resolvableGameState = resolvableGameStatePossibilitySet.Single().ResolvableGameState;
        if (resolvableGameState.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Game state is not unresolved; effect stack is empty");
        }

        return resolvableGameState;
    }
}