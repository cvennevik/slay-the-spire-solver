using System.Collections;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyParty : IEnumerable<Enemy>
{
    public Enemy GetEnemy(int enemyPosition) => _enemies[enemyPosition - 1];

    private readonly Enemy[] _enemies;

    public EnemyParty(params Enemy[] enemies)
    {
        _enemies = enemies;
    }

    public bool Has(EnemyId id) => _enemies.Any(enemy => enemy.Id == id);
    public EnemyParty Remove(EnemyId id) => new(_enemies.Where(enemy => enemy.Id != id).ToArray());

    public override bool Equals(object? obj)
    {
        return obj is EnemyParty otherParty && _enemies.SequenceEqual(otherParty._enemies);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public IEnumerator<Enemy> GetEnumerator()
    {
        return ((IEnumerable<Enemy>)_enemies).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _enemies.GetEnumerator();
    }

    public override string ToString()
    {
        return $"[{string.Join(",\n\t\t", _enemies.ToList())}]";
    }
}
