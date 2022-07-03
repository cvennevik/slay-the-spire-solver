using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayUntargetedCardAction : PlayCardAction
{
    public PlayUntargetedCardAction(GameState gameState, EffectStack effectStack) : base(gameState, effectStack) { }
    public PlayUntargetedCardAction(GameState gameState, params Effect[] effects) : base(gameState, effects) { }
}