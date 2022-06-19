using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.AI;

public static class Solver
{
    public static IAction GetBestAction(GameState gameState)
    {
        var legalActions = gameState.GetLegalActions();
        var endTurnAction = new EndTurnAction(gameState);
        var actionsExceptEndTurn = legalActions.Except(new[] { endTurnAction }).ToList();
        if (actionsExceptEndTurn.Any())
        {
            return actionsExceptEndTurn.First();
        }
        return endTurnAction;
    }
}
