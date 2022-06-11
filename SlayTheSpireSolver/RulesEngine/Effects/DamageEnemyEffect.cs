using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct DamageEnemyEffect : IEffect
{
    private readonly Enemy _target;
    private readonly Damage _damage;

    public DamageEnemyEffect(Enemy target, Damage damage)
    {
        _target = target;
        _damage = damage;
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        if (!gameState.EnemyParty.Contains(_target)) return new[] { gameState };

        var enemies = gameState.EnemyParty.Select(x => x).ToList();
        var targetEnemy = _target;
        var enemyIndex = enemies.FindIndex(enemy => enemy == targetEnemy);
        if (enemyIndex < 0) return new[] { gameState };

        enemies[enemyIndex] = targetEnemy.DealDamage(_damage);
        if (enemies[enemyIndex].Health.Amount <= 0)
        {
            enemies.RemoveAt(enemyIndex);
        }

        return new[] { gameState with { EnemyParty = new EnemyParty(enemies.ToArray()) } };
    }

    public IReadOnlyCollection<GameStateWithUnresolvedEffects> Resolve(GameState gameState)
    {
        var result = ApplyTo(gameState);
        return result.Select(x => new GameStateWithUnresolvedEffects(x)).ToList();
    }
}