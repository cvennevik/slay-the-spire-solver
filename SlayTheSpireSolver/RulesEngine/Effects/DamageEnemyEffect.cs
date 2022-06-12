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
        var targetEnemy = _target;
        if (gameState.EnemyParty.All(x => x.Id != targetEnemy))
        {
            return new[] { new GameStateWithEffectStack(gameState) };
        }

        var enemies = gameState.EnemyParty.Select(x => x).ToList();
        var enemyIndex = enemies.FindIndex(enemy => enemy.Id == targetEnemy);
        if (enemyIndex < 0)
        {
            return new[] { new GameStateWithEffectStack(gameState) };
        }

        enemies[enemyIndex] = enemies[enemyIndex].DealDamage(_damage);
        if (enemies[enemyIndex].Health.Amount <= 0)
        {
            enemies.RemoveAt(enemyIndex);
        }

        return new[]
        {
            new GameStateWithEffectStack(gameState with { EnemyParty = new EnemyParty(enemies.ToArray()) })
        };
    }
}