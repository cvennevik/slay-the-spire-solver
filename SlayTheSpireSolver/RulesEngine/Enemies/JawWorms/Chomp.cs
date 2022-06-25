using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record Chomp : IJawWormMove
{
    private static readonly Damage Damage = new(12);

    public GameState Resolve(GameState gameState)
    {
        if (Damage > gameState.PlayerArmor)
        {
            var remainingDamage = Damage - gameState.PlayerArmor;
            var newPlayerHealth = gameState.PlayerHealth - remainingDamage;
            return gameState with { PlayerHealth = newPlayerHealth, PlayerArmor = 0 };
        }

        return gameState with { PlayerArmor = gameState.PlayerArmor - Damage };
    }

    public EffectStack GetEffects(Enemy enemy)
    {
        return new EffectStack(new ReducePlayerHealthEffect(12));
    }
}
