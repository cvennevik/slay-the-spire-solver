﻿using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.AI;

public static class Solver
{
    public static IAction GetBestAction(GameState gameState)
    {
        var legalActions = gameState.GetLegalActions();
        var endTurnAction = new ActionWithEffectStack(gameState, new EndTurnEffect());
        var actionsExceptEndTurn = legalActions.Except(new[] { endTurnAction }).ToList();
        if (actionsExceptEndTurn.Any())
        {
            return actionsExceptEndTurn.First();
        }
        return endTurnAction;
    }
}
