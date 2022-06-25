using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamageEnemyEffect : IEffect
{
    private readonly EnemyId _target;
    private readonly Damage _damage;

    public DamageEnemyEffect(EnemyId target, Damage damage)
    {
        _target = target;
        _damage = damage;
    }

    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(_target))
        {
            return gameState;
        }

        var newGameState = new GameState
        {
            EnemyParty = gameState.EnemyParty.ModifyEnemy(_target, enemy => enemy.DealDamage(_damage))
        };

        if (newGameState.EnemyParty.Get(_target).Health.Amount <= 0)
        {
            return new[] { newGameState.WithEffects(new EffectStack(new KillEnemyEffect(_target))) };
        }

        return newGameState;
    }

    private Enemy DamageEnemy(Enemy enemy)
    {
        return enemy;
    }
}