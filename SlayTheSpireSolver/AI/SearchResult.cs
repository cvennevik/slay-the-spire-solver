namespace SlayTheSpireSolver.AI;

public record SearchResult
{
    public double ExpectedValue { get; init; }
    public int EvaluatedGameStates { get; init; }
    public int EvaluatedActions { get; init; }
    public int CacheHits { get; init; }
}
