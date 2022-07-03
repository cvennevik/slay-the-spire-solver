using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayCardAction : PlayerAction
{
    public PlayCardAction(GameState gameState, EffectStack effectStack) : base(gameState, effectStack) { }
    public PlayCardAction(GameState gameState, params Effect[] effects) : base(gameState, effects) { }
}