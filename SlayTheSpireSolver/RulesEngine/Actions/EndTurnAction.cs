using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record EndTurnAction : PlayerAction
{
    public EndTurnAction(GameState gameState) : base(gameState, new EndTurnEffect()) { }
}