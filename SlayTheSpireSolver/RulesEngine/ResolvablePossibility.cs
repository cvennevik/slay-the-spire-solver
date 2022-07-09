using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvablePossibility
{
    private ResolvableGameState ResolvableGameState { get; }
    public GameState GameState { get; }
    public Probability Probability { get; }

    public ResolvablePossibility(ResolvableGameState ResolvableGameState, Probability Probability)
    {
        this.ResolvableGameState = ResolvableGameState;
        this.GameState = ResolvableGameState.GameState with {EffectStack = ResolvableGameState.EffectStack};
        this.Probability = Probability;
    }

    public static implicit operator ResolvablePossibility(ResolvableGameState resolvableGameState) =>
        new(resolvableGameState, new Probability(1));
    public static implicit operator ResolvablePossibility(GameState gameState) =>
        new(gameState.WithEffects(), new Probability(1));
    public static implicit operator ResolvablePossibility(Possibility possibility) =>
        new(possibility.GameState.WithEffects(), possibility.Probability);
}