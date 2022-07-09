namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    private readonly GameState _unresolvedGameState;

    protected PlayerAction(GameState unresolvedGameState)
    {
        _unresolvedGameState = unresolvedGameState;
    }

    public IReadOnlyCollection<Possibility> Resolve() => _unresolvedGameState.Resolve();
}