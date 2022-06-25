using System;
using System.Linq;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

public static class ResolvableGameStatePossibilitySetExtensions
{
    public static GameState SingleResolvedState(this PossibilitySet possibilitySet)
    {
        var unresolvedGameState = possibilitySet.Single().ResolvableGameState;
        if (!unresolvedGameState.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Game state is not resolved; effect stack is not empty");
        }

        return unresolvedGameState.GameState;
    }

    public static ResolvableGameState SingleUnresolvedState(this PossibilitySet possibilitySet)
    {
        var resolvableGameState = possibilitySet.Single().ResolvableGameState;
        if (resolvableGameState.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Game state is not unresolved; effect stack is empty");
        }

        return resolvableGameState;
    }
}