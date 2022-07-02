using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record Possibility(GameState GameState, Probability Probability)
{
    public static implicit operator Possibility(GameState gameState) => new(gameState, new Probability(1));
}