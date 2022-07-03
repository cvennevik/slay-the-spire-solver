using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Actions;

namespace SlayTheSpireSolver.AI;

public static class Solver
{
    public static PlayerAction GetBestAction(GameState gameState)
    {
        var legalActions = gameState.GetLegalActions();
        var actionsExceptEndTurn = legalActions.Where(x => x is not EndTurnAction).ToList();
        if (actionsExceptEndTurn.Any())
        {
            return actionsExceptEndTurn.First();
        }
        return legalActions.First();
    }
}
