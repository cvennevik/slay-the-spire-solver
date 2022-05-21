using System;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies;

[TestFixture]
public class EnemyPartyTests
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
        var enemyParty2 = new EnemyParty(new JawWorm(), new JawWorm());
        Assert.AreNotEqual(enemyParty1, enemyParty2);
    }

    [Test]
    public void TestGetEnemy1()
    {
        var enemyParty = new EnemyParty(new JawWorm());

        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(3));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(2));
        Assert.AreEqual(new JawWorm(), enemyParty.GetEnemy(1));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(0));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(-1));
    }

    [Test]
    public void TestGetEnemy2()
    {
        var enemyParty = new EnemyParty(new JawWorm(), new JawWorm());

        Enemy _;
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(4));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(3));
        Assert.AreEqual(new JawWorm(), enemyParty.GetEnemy(2));
        Assert.AreEqual(new JawWorm(), enemyParty.GetEnemy(1));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(0));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(-1));
    }

    [Test]
    public void TestGetEnemy3()
    {
        var enemyParty = new EnemyParty();
        Enemy _;
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(2));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(1));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(0));
        Assert.Throws<IndexOutOfRangeException>(() => _ = enemyParty.GetEnemy(-1));
    }

    [Test]
    public void TestEnumerator1()
    {
        var enemyParty = new EnemyParty();
        Assert.AreEqual(0, enemyParty.Count());
    }

    [Test]
    public void TestEnumerator2()
    {
        var enemyParty = new EnemyParty(new JawWorm());
        Assert.AreEqual(1, enemyParty.Count());
        Assert.True(enemyParty.All(enemy => enemy == new JawWorm()));
    }

    [Test]
    public void TestEnumerator3()
    {
        var enemyParty = new EnemyParty(new JawWorm(), new JawWorm());
        Assert.AreEqual(2, enemyParty.Count());
        Assert.True(enemyParty.All(enemy => enemy == new JawWorm()));
    }
}
