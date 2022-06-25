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
        Assert.AreEqual(new Probability(1, 1), new Probability(1, 1));
        Assert.AreEqual(new Probability(0, 1), new Probability(0, 1));
        Assert.AreEqual(new Probability(3, 7), new Probability(3, 7));
        Assert.AreNotEqual(new Probability(3, 7), new Probability(3, 8));
    }
}