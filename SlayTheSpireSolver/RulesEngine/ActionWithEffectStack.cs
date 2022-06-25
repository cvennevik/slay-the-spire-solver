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

    private IReadOnlyList<GameStateWithEffectStack> ResolveTopEffect(
        GameStateWithEffectStack gameStateWithEffectStack)
    {
        return null;
    }
}