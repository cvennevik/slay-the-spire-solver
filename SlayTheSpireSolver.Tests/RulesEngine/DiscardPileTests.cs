using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class DiscardPileTests
{
    [Test]
    public void TestEquality1()
    {
        Assert.AreEqual(new DiscardPile(), new DiscardPile());
    }

    [Test]
    public void TestEquality2()
    {
        var discardPile1 = new DiscardPile();
        var discardPile2 = new DiscardPile(new StrikeCard());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality3()
    {
        var discardPile1 = new DiscardPile(new StrikeCard());
        var discardPile2 = new DiscardPile(new StrikeCard());
        Assert.AreEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality4()
    {
        var discardPile1 = new DiscardPile(new StrikeCard());
        var discardPile2 = new DiscardPile(new StrikeCard(), new StrikeCard());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality5()
    {
        var discardPile1 = new DiscardPile(new StrikeCard(), new StrikeCard());
        var discardPile2 = new DiscardPile(new StrikeCard(), new StrikeCard());
        Assert.AreEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality6()
    {
        var discardPile1 = new DiscardPile(new StrikeCard(), new StrikeCard());
        var discardPile2 = new DiscardPile(new StrikeCard(), new DefendCard());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality7()
    {
        var discardPile1 = new DiscardPile(new StrikeCard(), new StrikeCard());
        var discardPile2 = new DiscardPile(new StrikeCard(), new StrikeCard(), new DefendCard());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality8()
    {
        var discardPile1 = new DiscardPile(new StrikeCard(), new DefendCard());
        var discardPile2 = new DiscardPile(new StrikeCard(), new DefendCard());
        Assert.AreEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality9()
    {
        var discardPile1 = new DiscardPile(new DefendCard(), new StrikeCard());
        var discardPile2 = new DiscardPile(new StrikeCard(), new DefendCard());
        Assert.AreEqual(discardPile1, discardPile2);
    }
}
