using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record Action : GameStateWithEffectStack
{
    public Action(GameState gameState, EffectStack effectStack) : base(gameState, effectStack) { }
    public Action(GameState gameState, params IEffect[] effects) : base(gameState, effects) { }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        return ResolveEffects(new GameStateWithEffectStack(GameState, EffectStack));
    }
}