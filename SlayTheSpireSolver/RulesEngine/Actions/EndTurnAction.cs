using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record EndTurnAction : PlayerAction
{
    public EndTurnAction(GameState gameState, EffectStack effectStack) : base(gameState, effectStack) { }
    public EndTurnAction(GameState gameState, params Effect[] effects) : base(gameState, effects) { }
}