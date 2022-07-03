using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public abstract record PlayerAction
{
    private readonly ResolvableGameState _resolvableGameState;

    public PlayerAction(GameState gameState, EffectStack effectStack)
    {
        _resolvableGameState = new ResolvableGameState(gameState, effectStack);
    }

    public PlayerAction(GameState gameState, params Effect[] effects)
    {
        _resolvableGameState = new ResolvableGameState(gameState, effects);
    }

    public IReadOnlyCollection<Possibility> Resolve() => _resolvableGameState.Resolve();
}