namespace SlayTheSpireSolver.RulesEngine;

public interface IAction
{
    GameState[] ResolvePossibleStates();
}
