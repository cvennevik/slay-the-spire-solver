namespace SlayTheSpireSolver.AI;

public record TimedSearchResult : SearchResult
{
    public double ElapsedTime { get; init; }
}
