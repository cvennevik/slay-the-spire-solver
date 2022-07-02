namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record TargetEnemyEffect : IEffect
{
    public abstract ResolvablePossibilitySet Resolve(GameState gameState);
}