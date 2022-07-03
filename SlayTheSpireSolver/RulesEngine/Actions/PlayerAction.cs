namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    protected readonly ResolvableGameState ResolvableGameState;

    protected PlayerAction(ResolvableGameState resolvableGameState)
    {
        ResolvableGameState = resolvableGameState;
    }

    public IReadOnlyCollection<Possibility> Resolve() => ResolvableGameState.Resolve();
}