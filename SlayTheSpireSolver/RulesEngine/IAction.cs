namespace SlayTheSpireSolver.RulesEngine;

public interface IAction
{
    GameState Resolve();
}
