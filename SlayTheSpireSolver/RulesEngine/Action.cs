using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record Action(GameState GameState, EffectStack EffectStack)
{
    public Action(GameState gameState, params IEffect[] effects) : this(gameState,
        new EffectStack(effects)) { }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        return ResolveEffects(new GameStateWithEffectStack(GameState, EffectStack));
    }

    private IReadOnlyList<GameStateWithEffectStack> ResolveTopEffect(GameState gameState, EffectStack effectStack)
    {
        var (effect, remainingEffectStack) = effectStack.Pop();
        var outcomes = effect.Resolve(gameState);
        return outcomes.Select(gameStateWithAddedEffects =>
            gameStateWithAddedEffects.GameState.WithEffectStack(
                remainingEffectStack.Push(gameStateWithAddedEffects.EffectStack))).ToList();
    }

    private IReadOnlyList<GameStateWithEffectStack> ResolveTopEffect(GameStateWithEffectStack gameStateWithEffectStack)
    {
        return ResolveTopEffect(gameStateWithEffectStack.GameState, gameStateWithEffectStack.EffectStack);
    }

    private IReadOnlyList<GameState> ResolveEffects(GameStateWithEffectStack gameStateWithEffectStack)
    {
        if (gameStateWithEffectStack.EffectStack.IsEmpty())
        {
            return new[] { gameStateWithEffectStack.GameState };
        }

        return ResolveTopEffect(gameStateWithEffectStack).SelectMany(ResolveEffects).Distinct().ToList();
    }
}