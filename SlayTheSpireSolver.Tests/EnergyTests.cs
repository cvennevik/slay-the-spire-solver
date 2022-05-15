using NUnit.Framework;
using System;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class EnergyTests
{
    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-999)]
    public void AmountCannotBeNegative(int amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Energy(amount));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(999)]
    public void EnergyWithEqualAmountsAreEqual(int amount)
    {
        Assert.AreEqual(new Energy(amount), new Energy(amount));
    }

    [Test]

    [TestCase(0, 0, false)]
    [TestCase(0, 1, false)]
    [TestCase(0, 2, false)]
    [TestCase(1, 0, true)]
    [TestCase(1, 1, false)]
    [TestCase(1, 2, false)]
    [TestCase(2, 0, true)]
    [TestCase(2, 1, true)]
    [TestCase(2, 2, false)]
    public void TestGreaterThan(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Energy(amountA) > new Energy(amountB));
    }

    [Test]

    [TestCase(0, 0, true)]
    [TestCase(0, 1, false)]
    [TestCase(0, 2, false)]
    [TestCase(1, 0, true)]
    [TestCase(1, 1, true)]
    [TestCase(1, 2, false)]
    [TestCase(2, 0, true)]
    [TestCase(2, 1, true)]
    [TestCase(2, 2, true)]
    public void TestGreaterThanOrEqualTo(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Energy(amountA) >= new Energy(amountB));
    }

    [Test]
    [TestCase(0, 0, false)]
    [TestCase(0, 1, true)]
    [TestCase(0, 2, true)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, false)]
    [TestCase(1, 2, true)]
    [TestCase(2, 0, false)]
    [TestCase(2, 1, false)]
    [TestCase(2, 2, false)]
    public void TestLessThan(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Energy(amountA) < new Energy(amountB));
    }

    [Test]
    [TestCase(0, 0, true)]
    [TestCase(0, 1, true)]
    [TestCase(0, 2, true)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, true)]
    [TestCase(1, 2, true)]
    [TestCase(2, 0, false)]
    [TestCase(2, 1, false)]
    [TestCase(2, 2, true)]
    public void TestLessThanOrEqualTo(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Energy(amountA) <= new Energy(amountB));
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
        Assert.AreEqual(new Energy(expectedAmount), new Energy(amountA) + new Energy(amountB));
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
        Assert.AreEqual(new Energy(expectedAmount), new Energy(amountA) - new Energy(amountB));
    }
}
