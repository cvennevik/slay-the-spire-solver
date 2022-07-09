namespace SlayTheSpireSolver.RulesEngine.Effects;

public abstract record Effect
{
    public abstract PossibilitySet NewResolve(GameState gameState);
}