﻿using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies.JawWorms;

[TestFixture]
public class JawWormTests
{
    [Test]
    [TestCase(10, 6, 4)]
    [TestCase(8, 1, 7)]
    [TestCase(7, 10, -3)]
    [TestCase(1, 1, 0)]
    [TestCase(10, 0, 10)]
    public void DamageReducesHealth(int initialAmountOfHealth, int amountOfDamage, int expectedAmountOfHealth)
    {
        var jawWorm = new JawWorm { Health = new Health(initialAmountOfHealth) };
        var damagedJawWorm = jawWorm.DealDamage(new Damage(amountOfDamage));
        Assert.AreEqual(new JawWorm { Health = new Health(expectedAmountOfHealth) }, damagedJawWorm);
    }
}
