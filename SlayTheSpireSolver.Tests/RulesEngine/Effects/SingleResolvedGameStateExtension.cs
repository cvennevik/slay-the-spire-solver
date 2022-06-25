using System;
using System.Collections.Generic;
using System.Linq;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

public static class SingleResolvedGameStateExtension
{
    public static GameState SingleResolvedGameState(this ResolvableGameStateSet resolvableGameStateSet)
    {
        var unresolvedGameState = resolvableGameStateSet.Single();
        if (!unresolvedGameState.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Effect stack is not empty");
        }

        return unresolvedGameState.GameState;
    }
}