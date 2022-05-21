using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class DrawPileTests
{
    [Test]
    public void Test()
    {
        var DrawPile = new DrawPile();
        Assert.IsEmpty(DrawPile.Cards);
    }

    [Test]
    public void TestEquality1()
    {
        Assert.AreEqual(new DrawPile(), new DrawPile());
    }

    [Test]
    public void TestEquality2()
    {
        var DrawPile1 = new DrawPile();
        var DrawPile2 = new DrawPile(new StrikeCard());
        Assert.AreNotEqual(DrawPile1, DrawPile2);
    }

    [Test]
    public void TestEquality3()
    {
        var DrawPile1 = new DrawPile(new StrikeCard());
        var DrawPile2 = new DrawPile(new StrikeCard());
        Assert.AreEqual(DrawPile1, DrawPile2);
    }

    [Test]
    public void TestEquality4()
    {
        var DrawPile1 = new DrawPile(new StrikeCard());
        var DrawPile2 = new DrawPile(new StrikeCard(), new StrikeCard());
        Assert.AreNotEqual(DrawPile1, DrawPile2);
    }

    [Test]
    public void TestEquality5()
    {
        var DrawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
        var DrawPile2 = new DrawPile(new StrikeCard(), new StrikeCard());
        Assert.AreEqual(DrawPile1, DrawPile2);
    }

    [Test]
    public void TestEquality6()
    {
        var DrawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
        var DrawPile2 = new DrawPile(new StrikeCard(), new DefendCard());
        Assert.AreNotEqual(DrawPile1, DrawPile2);
    }

    [Test]
    public void TestEquality7()
    {
        var DrawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
        var DrawPile2 = new DrawPile(new StrikeCard(), new StrikeCard(), new DefendCard());
        Assert.AreNotEqual(DrawPile1, DrawPile2);
    }

    [Test]
    public void TestEquality8()
    {
        var DrawPile1 = new DrawPile(new StrikeCard(), new DefendCard());
        var DrawPile2 = new DrawPile(new StrikeCard(), new DefendCard());
        Assert.AreEqual(DrawPile1, DrawPile2);
    }

    [Test]
    public void EqualityIgnoresOrder()
    {
        var DrawPile1 = new DrawPile(new DefendCard(), new StrikeCard());
        var DrawPile2 = new DrawPile(new StrikeCard(), new DefendCard());
        Assert.AreEqual(DrawPile1, DrawPile2);
    }
}
