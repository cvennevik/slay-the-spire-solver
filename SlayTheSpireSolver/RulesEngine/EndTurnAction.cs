using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record EndTurnAction : IAction
{
    public GameState GameState { get; }

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver();
    }

    public EndTurnAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal EndTurn action");

        GameState = gameState;
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        GameState nextGameState = GameState;
        foreach (var enemy in GameState.EnemyParty)
        {
            nextGameState = enemy.GetIntendedMove().Resolve(GameState);
        }

        nextGameState = nextGameState with { Turn = new Turn(GameState.Turn.Number + 1) };
        return new[] { nextGameState };
    }
}
