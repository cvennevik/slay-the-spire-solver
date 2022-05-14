namespace SlayTheSpireSolver;

public record EndTurnAction : Action
{
    public GameState GameState { get; }

    public EndTurnAction(GameState gameState)
    {
        GameState = gameState;
    }

    public override GameState Resolve()
    {
        return new GameState { TurnNumber = GameState.TurnNumber + 1 };
    }
}
