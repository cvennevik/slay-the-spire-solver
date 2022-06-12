using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct DamageEnemyEffect : IEffect
{
    private readonly EnemyId _target;
    private readonly Damage _damage;

    public DamageEnemyEffect(EnemyId target, Damage damage)
    {
        _target = target;
        _damage = damage;
    }

    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(_target))
        {
            return new[] { gameState.WithEffectStack() };
        }

        var damage = _damage;
        var newEnemyParty = gameState.EnemyParty.ModifyEnemy(_target, enemy => enemy.DealDamage(damage));
        var damagedEnemy = newEnemyParty.Get(_target);
        if (damagedEnemy.Health.Amount <= 0)
        {
            newEnemyParty = newEnemyParty.Remove(_target);
        }

        return new[]
        {
            new GameStateWithEffectStack(gameState with { EnemyParty = newEnemyParty })
        };
    }
}