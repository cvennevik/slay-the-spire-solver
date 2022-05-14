namespace SlayTheSpireSolver;

public record GameState
{
    public Turn Turn { get; init; }

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        return new[] { new EndTurnAction(this) };
    }
}
