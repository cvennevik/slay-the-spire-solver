using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record ActionWithEffectStack(GameState GameState, EffectStack EffectStack) : IAction
{
    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        var remainingEffectStack = EffectStack;
        var workingGameState = GameState;

        while (remainingEffectStack != new EffectStack())
        {
            (var effect, remainingEffectStack) = remainingEffectStack.Pop();
            var gameStateWithAddedEffects = effect.Resolve(workingGameState).Single();
            workingGameState = gameStateWithAddedEffects.GameState;
            var addedEffects = gameStateWithAddedEffects.EffectStack;
            remainingEffectStack = remainingEffectStack.Push(addedEffects);
        }

        return new[] { workingGameState };
    }

    private IReadOnlyList<GameStateWithEffectStack> ResolveTopEffect(GameState gameState, EffectStack effectStack)
    {
        (var effect, var remainingEffectStack) = effectStack.Pop();
        var outcomes = effect.Resolve(gameState);
        return outcomes.Select(gameStateWithAddedEffects =>
            gameStateWithAddedEffects.GameState.WithEffectStack(
                remainingEffectStack.Push(gameStateWithAddedEffects.EffectStack))).ToList();
    }

    private IReadOnlyList<GameStateWithEffectStack> ResolveTopEffect(GameStateWithEffectStack gameStateWithEffectStack)
    {
        return ResolveTopEffect(gameStateWithEffectStack.GameState, gameStateWithEffectStack.EffectStack);
    }

    private IReadOnlyList<GameStateWithEffectStack> ResolveEffects(GameStateWithEffectStack gameStateWithEffectStack)
    {
        if (gameStateWithEffectStack.EffectStack == new EffectStack())
        {
            return new[] { gameStateWithEffectStack };
        }

        return ResolveTopEffect(gameStateWithEffectStack).SelectMany(ResolveEffects).ToList();
    }
}