namespace SlayTheSpireSolver.Cards.Defend;

public record DefendAction : IAction
{
    public GameState GameState { get; }

    public DefendAction(GameState gameState)
    {
        if (!gameState.Hand.Cards.Contains(new DefendCard())) throw new ArgumentException("No Defend card in hand");
        GameState = gameState;
    }

    public GameState Resolve()
    {
        throw new NotImplementedException();
    }
}
