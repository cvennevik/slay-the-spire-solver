namespace SlayTheSpireSolver.RulesEngine.Actions;

public interface PlayerAction
{
    public PossibilitySet Resolve();
}