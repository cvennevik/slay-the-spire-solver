using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record PossibleState
{
    public GameState GameState { get; }
    public Probability Probability { get; }

    public PossibleState(GameState gameState, Probability probability)
    {
        GameState = gameState;
        Probability = probability;
    }
}
