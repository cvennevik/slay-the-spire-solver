namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    private readonly GameState _unresolvedGameState;

    protected PlayerAction(GameState gameState)
    {
        _unresolvedGameState = gameState;
    }

    public IReadOnlyCollection<Possibility> Resolve() => _unresolvedGameState.Resolve();
}