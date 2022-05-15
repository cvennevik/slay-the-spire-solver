using NUnit.Framework;
using System;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class ArmorTests
{
    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-999)]
    public void AmmountCannotBeNegative(int amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Armor(amount));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void ArmorWithEqualAmountsAreEqual(int amount)
    {
        Assert.AreEqual(new Armor(amount), new Armor(amount));
    }
}
