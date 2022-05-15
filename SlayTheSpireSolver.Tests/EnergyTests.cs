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
}
