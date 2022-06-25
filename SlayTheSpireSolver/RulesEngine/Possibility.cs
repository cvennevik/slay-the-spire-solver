using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record Possibility(ResolvableGameState ResolvableGameState, Probability Probability)
{
    public static implicit operator Possibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
    public static implicit operator Possibility(GameState gameState) =>
        new(gameState, new Probability(1));
}