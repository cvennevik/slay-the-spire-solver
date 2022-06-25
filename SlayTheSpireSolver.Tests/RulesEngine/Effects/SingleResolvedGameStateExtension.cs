using System;
using System.Collections.Generic;
using System.Linq;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

public static class SingleResolvedGameStateExtension
{
    public static GameState SingleStableGameState(this IReadOnlyCollection<UnresolvedGameState> collection)
    {
        var gameStateWithUnresolvedEffects = collection.Single();
        if (!gameStateWithUnresolvedEffects.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Effect stack is not empty");
        }

        return gameStateWithUnresolvedEffects.GameState;
    }
}