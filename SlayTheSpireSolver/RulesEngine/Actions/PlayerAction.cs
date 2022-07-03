namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    protected readonly ResolvableGameState _resolvableGameState;

    protected PlayerAction(ResolvableGameState resolvableGameState)
    {
        _resolvableGameState = resolvableGameState;
    }

    public IReadOnlyCollection<Possibility> Resolve() => _resolvableGameState.Resolve();
}