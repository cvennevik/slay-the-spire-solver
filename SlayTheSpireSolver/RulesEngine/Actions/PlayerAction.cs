using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    private readonly ResolvableGameState _resolvableGameState;

    protected PlayerAction(GameState gameState)
    {
        _resolvableGameState = new ResolvableGameState(gameState);
    }

    public IReadOnlyCollection<Possibility> Resolve() => _resolvableGameState.Resolve();
}