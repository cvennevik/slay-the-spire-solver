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
            workingGameState = effect.Resolve(workingGameState).Select(x => x.GameState).Single();
        }
        return new[] { workingGameState };
    }
};