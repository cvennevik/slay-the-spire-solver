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
        return new GameState { TurnNumber = GameState.TurnNumber + 1 };
    }
}
