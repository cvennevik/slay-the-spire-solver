using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Values;

[TestFixture]
public class TurnTests
{
    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-99999)]
    public void DoesNotPermitNonPositiveTurnNumbers(int turnNumber)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Turn(turnNumber));
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(99999)]
    public void PermitsPositiveTurnNumbers(int turnNumber)
    {
        var turn = new Turn(turnNumber);
        Assert.AreEqual(turnNumber, turn.Number);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    public void TestEquality(int turnNumber)
    {
        Assert.AreEqual(new Turn(turnNumber), new Turn(turnNumber));
    }
}
