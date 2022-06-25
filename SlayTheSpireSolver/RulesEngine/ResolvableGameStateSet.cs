namespace SlayTheSpireSolver.RulesEngine;

public class ResolvableGameStateSet
{
    private readonly ResolvableGameState[] _resolvableGameStates;

    public ResolvableGameStateSet(params ResolvableGameState[] resolvableGameStates)
    {
        _resolvableGameStates = resolvableGameStates;
    }
}