using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamageEnemyEffect : IEffect
{
    private readonly EnemyId _targetId;
    private readonly Damage _damage;

    public DamageEnemyEffect(EnemyId targetId, Damage damage)
    {
        _targetId = targetId;
        _damage = damage;
    }

    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(_targetId))
        {
            return gameState;
        }

        var newGameState = gameState.ModifyEnemy(_targetId, enemy => DamageEnemy(enemy, _damage));

        if (newGameState.EnemyParty.Get(_targetId).Health.Amount <= 0)
        {
            return new[] { newGameState.WithEffects(new EffectStack(new KillEnemyEffect(_targetId))) };
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