using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayTargetedCardAction : PlayCardAction
{
    public PlayTargetedCardAction(GameState gameState, EffectStack effectStack) : base(gameState, effectStack) { }
    public PlayTargetedCardAction(GameState gameState, params Effect[] effects) : base(gameState, effects) { }
}