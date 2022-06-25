namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct NullEffect : IEffect
{
    public PossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}