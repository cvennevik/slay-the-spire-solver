using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class IncrementTurnExtension
{
    public static GameState IncrementTurn(this GameState gameState)
    {
        return gameState with { Turn = gameState.Turn.Number + 1 };
    }
}
