using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayCardAction : PlayerAction
{
    public PlayCardAction(GameState gameState, EffectStack effectStack) : base(new ResolvableGameState(gameState, effectStack)) { }
    public PlayCardAction(GameState gameState, params Effect[] effects) : base(new ResolvableGameState(gameState, effects)) { }
}