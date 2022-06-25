using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies;

[TestFixture]
public class EnemyIdTests
{
    [Test]
    public void TestEquality()
    {
        var enemyId1 = EnemyId.New();
        var enemyId2 = EnemyId.New();
        Assert.AreEqual(enemyId1, enemyId1);
        Assert.AreEqual(enemyId2, enemyId2);
        Assert.AreNotEqual(enemyId1, enemyId2);
    }

    [Test]
    public void NewEnemiesAreEqual()
    {
        Assert.AreEqual(new JawWorm(), new JawWorm());
    }

    [Test]
    public void NewEnemiesHaveDefaultId()
    {
        Assert.AreEqual(EnemyId.Default, new JawWorm().Id);
        Assert.AreEqual(new JawWorm().Id, new JawWorm().Id);
    }

    [Test]
    public void EnemiesWithDifferentIdsAreNotEqual()
    {
        Assert.AreNotEqual(new JawWorm { Id = EnemyId.New() }, new JawWorm { Id = EnemyId.New() });
    }
}