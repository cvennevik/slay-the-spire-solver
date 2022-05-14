namespace SlayTheSpireSolver;

public record EndTurnAction : IAction
{
    public GameState GameState { get; }

    public EndTurnAction(GameState gameState)
    {
        if (gameState.Enemy == null) throw new ArgumentException();

        GameState = gameState;
    }

    public GameState Resolve()
    {
        GameState nextGameState = GameState;
        if (GameState.Enemy != null)
        {
            nextGameState = GameState.Enemy.GetIntendedMove().Resolve(GameState);
        }
        return nextGameState with { Turn = new Turn(GameState.Turn.Number + 1) };
    }
}
