using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record EndTurnAction : ActionWithEffectStack
{
    private static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver();
    }

    public EndTurnAction(GameState gameState) : base(gameState, new EffectStack(new EndTurnEffect()))
    {
    }
}
