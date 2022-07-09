using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.TestHelpers;

internal static class ResolvableGameStatePossibilitySetExtensions
{
    internal static GameState SingleResolvedState(this ResolvablePossibilitySet resolvablePossibilitySet)
    {
        var gameState = resolvablePossibilitySet.Single().GameState;
        if (!gameState.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Game state is not resolved; effect stack is not empty");
        }

        return gameState;
    }

    internal static ResolvableGameState SingleUnresolvedState(this ResolvablePossibilitySet resolvablePossibilitySet)
    {
        var resolvableGameState = resolvablePossibilitySet.Single().ResolvableGameState;
        if (resolvableGameState.EffectStack.IsEmpty())
        {
            throw new ArgumentException("Game state is not unresolved; effect stack is empty");
        }

        return resolvableGameState;
    }
}