using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies.JawWorms;

[TestFixture]
public class JawWormTests
{
    [Test]
    public void NewEnemiesAreEqual()
    {
        Assert.AreEqual(new JawWorm(), new JawWorm());
    }

    [Test]
    public void NewEnemiesHaveSameId()
    {
        Assert.AreEqual(new JawWorm().Id, new JawWorm().Id);
    }

    [Test]
    public void EnemiesWithDifferentIdsAreNotEqual()
    {
        Assert.AreNotEqual(new JawWorm { Id = EnemyId.New() }, new JawWorm { Id = EnemyId.New() });
    }

    [Test]
    [TestCase(0, 10, 6, 0, 4)]
    [TestCase(0, 8, 1, 0, 7)]
    [TestCase(0, 7, 10, 0, -3)]
    [TestCase(0, 1, 1, 0, 0)]
    [TestCase(0, 10, 0, 0, 10)]
    [TestCase(10, 10, 6, 4, 10)]
    [TestCase(5, 10, 5, 0, 10)]
    [TestCase(5, 10, 10, 0, 5)]
    [TestCase(5, 10, 20, 0, -5)]
    [TestCase(10, 10, 0, 10, 10)]
    public void DamageReducesArmorAndHealth(int initialArmor, int initialHealth, int damage,
        int expectedArmor, int expectedHealth)
    {
        var jawWorm = new JawWorm { Armor = new Armor(initialArmor), Health = new Health(initialHealth) };
        var damagedJawWorm = jawWorm.DealDamage(new Damage(damage));
        Assert.AreEqual(new JawWorm { Armor = new Armor(expectedArmor), Health = new Health(expectedHealth) },
            damagedJawWorm);
    }
}
