using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamagePlayerEffect(Damage Damage) : IEffect
{
    public PossibilitySet Resolve(GameState gameState)
    {
        if (Damage > gameState.PlayerArmor)
        {
            var remainingDamage = Damage - gameState.PlayerArmor;
            return gameState with { PlayerArmor = 0, PlayerHealth = gameState.PlayerHealth - remainingDamage };
        }

        return gameState with { PlayerArmor = gameState.PlayerArmor - Damage };
    }
}