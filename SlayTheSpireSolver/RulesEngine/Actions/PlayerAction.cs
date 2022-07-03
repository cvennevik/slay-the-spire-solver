using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayerAction : ResolvableGameState
{
    public PlayerAction(GameState gameState, EffectStack effectStack) : base(gameState, effectStack) { }
    public PlayerAction(GameState gameState, params Effect[] effects) : base(gameState, effects) { }
}