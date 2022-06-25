using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AttackPlayerEffectTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new AttackPlayerEffect(EnemyId.Default, new Damage(1)),
            new AttackPlayerEffect(EnemyId.Default, new Damage(1)));
        Assert.AreNotEqual(new AttackPlayerEffect(EnemyId.Default, new Damage(1)),
            new AttackPlayerEffect(EnemyId.Default, new Damage(2)));
        Assert.AreNotEqual(new AttackPlayerEffect(EnemyId.Default, new Damage(1)),
            new AttackPlayerEffect(EnemyId.New(), new Damage(1)));
    }
}