namespace SlayTheSpireSolver;

public record PossibleGameState
{
    public GameState GameState { get; }
    public double Probability { get; }

    public PossibleGameState(GameState gameState, double probability)
    {
        if (probability > 1 || probability < 0) throw new ArgumentOutOfRangeException(nameof(probability));
        GameState = gameState;
        Probability = probability;
    }
}
