namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct NullEffect : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}