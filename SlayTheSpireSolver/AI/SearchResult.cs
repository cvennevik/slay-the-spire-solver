namespace SlayTheSpireSolver.AI;

public record SearchResult
{
    public double ExpectedValue { get; init; }
    public int EvalutedGameStates { get; init; }
}
