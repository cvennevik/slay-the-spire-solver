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

    
    [Test]
    [TestCase(1)]
    [TestCase(5)]
    public void ImplicitlyConvertedTurnEqualsExplicitTurn(int turnNumber)
    {
        Turn turn = turnNumber;
        Assert.AreEqual(new Turn(turnNumber), turn);
        Assert.True(new Turn(turnNumber) == turnNumber);
        Assert.True(turnNumber == new Turn(turnNumber));
    }

    [Test]
    public void ImplicitlyConvertsNonPositiveTurnNumbersToTurnOne()
    {
        Assert.True(new Turn(1) == 0);
        Assert.True(new Turn(1) == -1);
        Assert.True(new Turn(1) == -5);
    }
}
