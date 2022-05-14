namespace SlayTheSpireSolver;

public record GameState
{
    public Player Player { get; init; }
    public Turn Turn { get; init; }

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        return new[] { new EndTurnAction(this) };
    }
}
