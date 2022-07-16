namespace SlayTheSpireSolver.AI;

public record TimedSearchResult : SearchResult
{
    public double ElapsedMilliseconds { get; init; }
}
