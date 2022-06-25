using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamagePlayerEffect(Damage Damage) : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState with { PlayerHealth = gameState.PlayerHealth - Damage };
    }
}