namespace SlayTheSpireSolver;

public record GameState
{
    public IReadOnlyCollection<Action> GetLegalActions()
    {
        return new[] { new EndTurnAction() };
    }
}
