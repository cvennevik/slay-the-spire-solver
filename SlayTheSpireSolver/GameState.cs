namespace SlayTheSpireSolver;

public record GameState
{
    public Player Player { get; init; }
    public IEnemy Enemy { get; init; }
    public Turn Turn { get; init; } = new Turn(1);

    public IReadOnlyCollection<IAction> GetLegalActions()
    {
        return new[] { new EndTurnAction(this) };
    }
}
