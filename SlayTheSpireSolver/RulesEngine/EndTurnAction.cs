using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record EndTurnAction : ActionWithEffectStack
{
    public EndTurnAction(GameState gameState) : base(gameState, new EffectStack(new EndTurnEffect()))
    {
    }
}
