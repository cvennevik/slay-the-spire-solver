namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    private readonly ResolvableGameState _resolvableGameState;
    private readonly GameState _unresolvedGameState;

    protected PlayerAction(GameState gameState)
    {
        _unresolvedGameState = gameState;
        _resolvableGameState = new ResolvableGameState(gameState);
    }

    public IReadOnlyCollection<Possibility> Resolve() => _resolvableGameState.Resolve();
}