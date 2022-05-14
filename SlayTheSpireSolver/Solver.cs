namespace SlayTheSpireSolver;

public class Solver
{
    public static IAction GetBestAction(GameState gameState)
    {
        return new EndTurnAction(gameState);
    }
}
