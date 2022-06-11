namespace SlayTheSpireSolver.RulesEngine;

public interface IAction
{
    IReadOnlyList<GameState> ResolveToPossibleStates();
}
