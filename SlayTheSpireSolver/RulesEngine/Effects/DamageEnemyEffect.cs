using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamageEnemyEffect(EnemyId TargetId, Damage Damage) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(TargetId))
        {
            return gameState;
        }

        var newGameState = gameState.ModifyEnemy(TargetId, enemy => DamageEnemy(enemy, Damage));

        if (newGameState.EnemyParty.Get(TargetId).Health.Amount <= 0)
        {
            return new[] { newGameState.WithEffects(new EffectStack(new KillEnemyEffect(TargetId))) };
        }

        return newGameState;
    }

    private static Enemy DamageEnemy(Enemy enemy, Damage damage)
    {
        if (damage < enemy.Armor)
        {
            return enemy with { Armor = enemy.Armor - damage };
        }

        var remainingDamage = damage - enemy.Armor;
        return enemy with { Armor = 0, Health = enemy.Health - remainingDamage };

    }
}