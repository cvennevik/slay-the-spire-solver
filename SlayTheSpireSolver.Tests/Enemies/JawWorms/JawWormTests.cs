using NUnit.Framework;
using SlayTheSpireSolver.Enemies.JawWorms;
using System;

namespace SlayTheSpireSolver.Tests.Enemies.JawWorms;

[TestFixture]
public class JawWormTests
{
    [Test]
    [TestCase(10, 6, 4)]
    [TestCase(8, 1, 7)]
    [TestCase(7, 10, -3)]
    [TestCase(1, 1, 0)]
    [TestCase(10, 0, 10)]
    public void DamageReducesHealth(int initialHealthValue, int damageValue, int expectedHealthValue)
    {
        var jawWorm = new JawWorm { Health = new Health(initialHealthValue) };
        var damagedJawWorm = jawWorm.Damage(damageValue);
        Assert.AreEqual(new JawWorm { Health = new Health(expectedHealthValue) }, damagedJawWorm);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-999)]
    public void DamageCannotBeNegative(int damageValue)
    {
        var jawWorm = new JawWorm { Health = new Health(10) };
        Assert.Throws<ArgumentOutOfRangeException>(() => jawWorm.Damage(damageValue));
    }
}
