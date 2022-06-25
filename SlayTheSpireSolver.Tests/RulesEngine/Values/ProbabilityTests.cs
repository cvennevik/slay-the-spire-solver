using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Values;

[TestFixture]
public class ProbabilityTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new Probability(1), new Probability(1));
        Assert.AreEqual(new Probability(0), new Probability(0));
        Assert.AreEqual(new Probability(0.5), new Probability(0.5));
        Assert.AreNotEqual(new Probability(1), new Probability(0));
    }

    [Test]
    public void ThrowsExceptionForValueAboveOne()
    {
        Assert.Throws<ArgumentException>(() => new Probability(1.01));
    }
}