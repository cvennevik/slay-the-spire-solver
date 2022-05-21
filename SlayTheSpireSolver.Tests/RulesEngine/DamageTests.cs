using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class DamageTests
{
    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-999)]
    public void AmmountCannotBeNegative(int amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Damage(amount));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void DamageWithEqualAmountsAreEqual(int amount)
    {
        Assert.AreEqual(new Damage(amount), new Damage(amount));
    }
}