using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record GameStatePossibility(GameState GameState, Probability Probability)
{
    public static implicit operator GameStatePossibility(GameState gameState) => new(gameState, new Probability(1));
}