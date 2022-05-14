using NUnit.Framework;
using System;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class ArmorTests
{
    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-99999)]
    public void DoesNotPermitNegativeArmorValues(int armorValue)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Armor(armorValue));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(99999)]
    public void PermitsNonNegativeArmorValues(int armorValue)
    {
        var armor = new Armor(armorValue);
        Assert.AreEqual(armorValue, armor.Value);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    public void TestEquality(int armorValue)
    {
        Assert.AreEqual(new Armor(armorValue), new Armor(armorValue));
    }
}
