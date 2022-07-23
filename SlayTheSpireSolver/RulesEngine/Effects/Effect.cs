namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect
{
    public abstract PossibilitySet Resolve(GameState gameState);
}