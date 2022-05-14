namespace SlayTheSpireSolver;

public record GameState
{
    public TurnNumber TurnNumber { get; init; }

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        return new[] { new EndTurnAction(this) };
    }
}

public readonly record struct TurnNumber(int Value);
