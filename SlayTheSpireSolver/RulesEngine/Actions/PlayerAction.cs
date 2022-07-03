using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    private readonly ResolvableGameState _resolvableGameState;

    protected PlayerAction(GameState gameState, EffectStack effectStack)
    {
        _resolvableGameState = new ResolvableGameState(gameState, effectStack);
    }

    public IReadOnlyCollection<Possibility> Resolve() => _resolvableGameState.Resolve();
}