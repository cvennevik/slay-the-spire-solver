using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class HandTests
{
    [Test]
    public void TestEquality1()
    {
        Assert.AreEqual(new Hand(), new Hand());
    }

    [Test]
    public void TestEquality2()
    {
        var hand1 = new Hand();
        var hand2 = new Hand { Cards = new[] { new StrikeCard() } };
        Assert.AreNotEqual(hand1, hand2);
    }

    [Test]
    public void TestEquality3()
    {
        var hand1 = new Hand { Cards = new[] { new StrikeCard() } };
        var hand2 = new Hand { Cards = new[] { new StrikeCard() } };
        Assert.AreEqual(hand1, hand2);
    }

    [Test]
    public void TestEquality4()
    {
        var hand1 = new Hand { Cards = new[] { new StrikeCard() } };
        var hand2 = new Hand { Cards = new[] { new StrikeCard(), new StrikeCard() } };
        Assert.AreNotEqual(hand1, hand2);
    }

    [Test]
    public void TestEquality5()
    {
        var hand1 = new Hand { Cards = new[] { new StrikeCard(), new StrikeCard() } };
        var hand2 = new Hand { Cards = new[] { new StrikeCard(), new StrikeCard() } };
        Assert.AreNotEqual(hand1, hand2);
    }
}
