using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine;

public record EndTurnAction : ActionWithEffectStack
{
    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver();
    }

    public EndTurnAction(GameState gameState) : base(gameState, new EffectStack(new EndTurnEffect()))
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal EndTurn action");

        GameState = gameState;
    }
}
