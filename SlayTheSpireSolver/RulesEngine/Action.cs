using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record Action : ResolvableGameState
{
    public Action(GameState gameState, EffectStack effectStack) : base(gameState, effectStack) { }
    public Action(GameState gameState, params IEffect[] effects) : base(gameState, effects) { }
}