namespace SlayTheSpireSolver;

public record GameState
{
    public int TurnNumber { get; init; }

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        return new[] { new EndTurnAction(this) };
    }
}
