namespace SlayTheSpireSolver;

public record GameState
{
    public int TurnNumber { get; init; }

    public IReadOnlyCollection<Action> GetLegalActions()
    {
        return new[] { new EndTurnAction(this) };
    }
}
