namespace SlayTheSpireSolver.Cards.Strike;

public record StrikeAction : IAction
{
    public GameState GameState { get; }

    public StrikeAction(GameState gameState)
    {
        GameState = gameState;
    }

    public GameState Resolve()
    {
        throw new NotImplementedException();
    }
}
