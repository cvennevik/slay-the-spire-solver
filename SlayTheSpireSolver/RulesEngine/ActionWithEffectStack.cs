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
        }

        return new[] { workingGameState };
    }
}