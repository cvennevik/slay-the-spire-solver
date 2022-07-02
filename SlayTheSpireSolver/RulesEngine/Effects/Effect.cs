namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect : IEffect
{
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);
}