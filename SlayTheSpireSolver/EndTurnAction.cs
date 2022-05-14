namespace SlayTheSpireSolver;

public record EndTurnAction : IAction
{
    public GameState GameState { get; }

    public EndTurnAction(GameState gameState)
    {
        GameState = gameState;
    }

    public GameState Resolve()
    {
        return GameState with { Turn = new Turn(GameState.Turn.Number + 1) };
    }
}
