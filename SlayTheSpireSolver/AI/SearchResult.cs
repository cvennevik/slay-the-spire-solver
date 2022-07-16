namespace SlayTheSpireSolver.AI;

public record SearchResult
{
    public double ExpectedValue { get; init; }
    public int EvaluatedGameStates { get; init; }
    public int EvaluatedActions { get; init; }
    public double ElapsedMilliseconds { get; init; }
    public int GameStateDepthLimit { get; init; }

    public override string ToString()
    {
        return $@"{{
    ExpectedValue: {ExpectedValue}
    EvaluatedGameStates: {EvaluatedGameStates}
    EvaluatedActions: {EvaluatedActions}
    ElapsedMilliseconds: {ElapsedMilliseconds}
    GameStateDepthLimit: {GameStateDepthLimit}
}}";
    }
}
