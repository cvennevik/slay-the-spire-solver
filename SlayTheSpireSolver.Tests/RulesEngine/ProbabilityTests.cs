using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class ProbabilityTests
{
    [Test]
    public void TestEquality1()
    {
        Assert.AreEqual(new Probability(1), new Probability(1));
    }

    [Test]
    public void TestEquality2()
    {
        Assert.AreNotEqual(new Probability(1), new Probability(0));
    }

    [Test]
    [TestCase(-999)]
    [TestCase(-1)]
    [TestCase(-0.001)]
    [TestCase(1.001)]
    [TestCase(2)]
    [TestCase(999)]
    public void ProbabilityAboveOneOrBelowZeroNotPermitted(double value)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Probability(value));
    }
}
