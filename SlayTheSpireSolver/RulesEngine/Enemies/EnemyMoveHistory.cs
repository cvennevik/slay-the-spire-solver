using System.Collections;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public class EnemyMoveHistory : IReadOnlyList<EnemyMove>
{
    private readonly List<EnemyMove> _moves;
    private readonly int _hashCode;

    public EnemyMoveHistory(params EnemyMove[] moves)
    {
        _moves = moves.ToList();
        _hashCode = moves.Aggregate(0, HashCode.Combine);
    }

    public int Count => _moves.Count;
    public EnemyMove this[int index] => _moves[index];

    public IEnumerator<EnemyMove> GetEnumerator()
    {
        return _moves.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _moves.GetEnumerator();
    }

    public static implicit operator EnemyMoveHistory(EnemyMove[] moves)
    {
        return new EnemyMoveHistory(moves);
    }

    public static bool operator ==(EnemyMoveHistory a, EnemyMoveHistory b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(EnemyMoveHistory a, EnemyMoveHistory b)
    {
        return !a.Equals(b);
    }

    public override bool Equals(object? obj)
    {
        return obj is EnemyMoveHistory otherHistory && this.SequenceEqual(otherHistory);
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(",", _moves)}]";
    }
}

[TestFixture]
internal class EnemyMoveHistoryTests
{
    [Test]
    public void EmptyHistoriesAreEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory();
        var enemyMoveHistory2 = new EnemyMoveHistory();
        Assert.AreEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void SingleMoveHistoriesAreEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp());
        Assert.AreEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void SingleMoveHistoriesAreDifferent()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Bellow());
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void DifferentLengthHistoriesAreDifferent1()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory();
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void DifferentLengthHistoriesAreDifferent2()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp(), new Chomp());
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void MultipleMoveHistoriesAreEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp(), new Bellow());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp(), new Bellow());
        Assert.AreEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void MultipleMoveHistoriesWithDifferentOrderAreNotEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Bellow(), new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp(), new Bellow());
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }
}