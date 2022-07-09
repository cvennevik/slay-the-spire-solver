using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvablePossibility
{
    public GameState GameState { get; }
    public Probability Probability { get; }

    public ResolvablePossibility(GameState GameState, Probability Probability)
    {
        this.GameState = GameState;
        this.Probability = Probability;
    }

    public static implicit operator ResolvablePossibility(Possibility possibility) =>
        new(possibility.GameState, possibility.Probability);
}