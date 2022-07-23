namespace SlayTheSpireSolver.RulesEngine.Effects;

public interface Effect
{
    public PossibilitySet Resolve(GameState gameState);
}