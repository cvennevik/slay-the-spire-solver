using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamagePlayerEffect(Damage Damage) : IEffect
{
    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        var result = gameState with { PlayerHealth = gameState.PlayerHealth - Damage };
        return new[] { result.WithEffectStack() };
    }
}