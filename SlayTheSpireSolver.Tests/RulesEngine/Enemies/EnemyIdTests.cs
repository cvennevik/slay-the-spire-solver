using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies;

[TestFixture]
public class EnemyIdTests
{
    [Test]
    public void TestEquality()
    {
        var enemyId1 = new EnemyId();
        var enemyId2 = new EnemyId();
        Assert.AreEqual(enemyId1, enemyId1);
        Assert.AreEqual(enemyId2, enemyId2);
        Assert.AreNotEqual(enemyId1, enemyId2);
    }
}