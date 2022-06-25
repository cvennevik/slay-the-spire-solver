using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using Action = SlayTheSpireSolver.RulesEngine.Action;

namespace SlayTheSpireSolver.AI;

public static class Solver
{
    public static Action GetBestAction(GameState gameState)
    {
        var legalActions = gameState.GetLegalActions();
        var endTurnAction = new Action(gameState, new EndTurnEffect());
        var actionsExceptEndTurn = legalActions.Except(new[] { endTurnAction }).ToList();
        if (actionsExceptEndTurn.Any())
        {
            return actionsExceptEndTurn.First();
        }
        return endTurnAction;
    }
}
