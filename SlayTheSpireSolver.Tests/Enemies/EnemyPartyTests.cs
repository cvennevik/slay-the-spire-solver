using NUnit.Framework;
using SlayTheSpireSolver.Enemies;
using SlayTheSpireSolver.Enemies.JawWorms;
using System;

namespace SlayTheSpireSolver.Tests.Enemies;

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
    public void TestCount1()
    {
        var enemyParty = new EnemyParty();
        Assert.AreEqual(0, enemyParty.Count);
    }

    [Test]
    public void TestCount2()
    {
        var enemyParty = new EnemyParty(new JawWorm());
        Assert.AreEqual(1, enemyParty.Count);
    }

    [Test]
    public void TestCount3()
    {
        var enemyParty = new EnemyParty(new JawWorm(), new JawWorm(), new JawWorm());
        Assert.AreEqual(3, enemyParty.Count);
    }
}
