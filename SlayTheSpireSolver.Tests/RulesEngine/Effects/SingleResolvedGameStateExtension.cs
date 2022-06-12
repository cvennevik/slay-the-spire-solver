using System;
using System.Collections.Generic;
using System.Linq;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

public static class SingleResolvedGameStateExtension
{
    public static GameState SingleResolvedGameState(this IReadOnlyCollection<GameStateWithEffectStack> collection)
    {
        var gameStateWithUnresolvedEffects = collection.Single();
        if (gameStateWithUnresolvedEffects.UnresolvedEffects.Any())
        {
            throw new ArgumentException("GameStateWithUnresolvedEffects has unresolved effects");
        }

        return gameStateWithUnresolvedEffects.GameState;
    }
}