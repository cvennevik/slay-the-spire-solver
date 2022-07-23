using System.Collections;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyParty : IEnumerable<Enemy>
{
    private readonly Enemy[] _enemies;
    private readonly int _hashCode;

    public static implicit operator EnemyParty(Enemy[] enemies)
    {
        return new EnemyParty(enemies);
    }

    public EnemyParty(params Enemy[] enemies)
    {
        if (enemies.Select(x => x.Id).Distinct().Count() != enemies.Length)
            throw new ArgumentException("Not all enemy IDs are unique");

        _enemies = enemies;
        _hashCode = _enemies.Aggregate(0, HashCode.Combine);
    }

    public bool Has(EnemyId id)
    {
        return _enemies.Any(enemy => enemy.Id == id);
    }

    public Enemy Get(EnemyId id)
    {
        return _enemies.First(x => x.Id == id);
    }

    public EnemyParty Remove(EnemyId id)
    {
        return new EnemyParty(_enemies.Where(enemy => enemy.Id != id).ToArray());
    }

    public EnemyParty ModifyEnemy(EnemyId id, Func<Enemy, Enemy> modifier)
    {
        return new EnemyParty(_enemies.Select(enemy => enemy.Id == id ? modifier(enemy) : enemy).ToArray());
    }

    public override bool Equals(object? obj)
    {
        return obj is EnemyParty otherParty &&
               _hashCode == otherParty._hashCode &&
               _enemies.SequenceEqual(otherParty._enemies);
    }

    public override int GetHashCode()
    {
        return _hashCode;
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

[TestFixture]
internal class EnemyPartyTests
{
    [Test]
    public void TestEquality1()
    {
        Assert.AreEqual(new EnemyParty(), new EnemyParty());
    }

    [Test]
    public void TestEquality2()
    {
        var enemyParty1 = new EnemyParty(new JawWorm());
        var enemyParty2 = new EnemyParty(new JawWorm());
        Assert.AreEqual(enemyParty1, enemyParty2);
    }

    [Test]
    public void TestEquality3()
    {
        var enemyParty1 = new EnemyParty(new JawWorm());
        var enemyParty2 = new EnemyParty(new JawWorm(), new JawWorm { Id = EnemyId.New() });
        Assert.AreNotEqual(enemyParty1, enemyParty2);
    }
}