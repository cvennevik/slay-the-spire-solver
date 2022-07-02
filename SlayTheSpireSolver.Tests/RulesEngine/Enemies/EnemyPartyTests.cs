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
}
