namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct NullEffect : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}