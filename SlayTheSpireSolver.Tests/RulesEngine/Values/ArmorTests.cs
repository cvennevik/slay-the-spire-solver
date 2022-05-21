using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Values;

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

    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, 1, 1)]
    [TestCase(0, 2, 2)]
    [TestCase(1, 0, 1)]
    [TestCase(1, 1, 2)]
    [TestCase(1, 2, 3)]
    [TestCase(2, 0, 2)]
    [TestCase(2, 1, 3)]
    [TestCase(2, 2, 4)]
    public void TestPlus(int amountA, int amountB, int expectedAmount)
    {
        Assert.AreEqual(new Armor(expectedAmount), new Armor(amountA) + new Armor(amountB));
    }

    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, 1, 0)]
    [TestCase(0, 2, 0)]
    [TestCase(1, 0, 1)]
    [TestCase(1, 1, 0)]
    [TestCase(1, 2, 0)]
    [TestCase(2, 0, 2)]
    [TestCase(2, 1, 1)]
    [TestCase(2, 2, 0)]
    public void TestMinus(int amountA, int amountB, int expectedAmount)
    {
        Assert.AreEqual(new Armor(expectedAmount), new Armor(amountA) - new Armor(amountB));
    }
}
