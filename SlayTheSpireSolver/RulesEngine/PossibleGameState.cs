namespace SlayTheSpireSolver.RulesEngine;

public record PossibleGameState
{
    public GameState GameState { get; }
    public Probability Probability { get; }

    public PossibleGameState(GameState gameState, Probability probability)
    {
        GameState = gameState;
        Probability = probability;
    }
}
