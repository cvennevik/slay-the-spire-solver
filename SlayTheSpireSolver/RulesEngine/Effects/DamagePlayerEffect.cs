using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamagePlayerEffect(Damage Damage) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        var result = gameState with { PlayerHealth = gameState.PlayerHealth - Damage };
        return new[] { result.WithEffectStack() };
    }
}