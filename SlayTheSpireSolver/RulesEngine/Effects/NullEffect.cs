namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct NullEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return new[] { new ResolvableGameState(gameState) };
    }
}