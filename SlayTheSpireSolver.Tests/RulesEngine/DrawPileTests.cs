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
        var drawPile = new DrawPile();
        Assert.IsEmpty(drawPile.Cards);
    }

    [Test]
    public void TestEquality1()
    {
        Assert.AreEqual(new DrawPile(), new DrawPile());
    }

    [Test]
    public void TestEquality2()
    {
        var drawPile1 = new DrawPile();
        var drawPile2 = new DrawPile(new StrikeCard());
        Assert.AreNotEqual(drawPile1, drawPile2);
    }

    [Test]
    public void TestEquality3()
    {
        var drawPile1 = new DrawPile(new StrikeCard());
        var drawPile2 = new DrawPile(new StrikeCard());
        Assert.AreEqual(drawPile1, drawPile2);
    }

    [Test]
    public void TestEquality4()
    {
        var drawPile1 = new DrawPile(new StrikeCard());
        var drawPile2 = new DrawPile(new StrikeCard(), new StrikeCard());
        Assert.AreNotEqual(drawPile1, drawPile2);
    }

    [Test]
    public void TestEquality5()
    {
        var drawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
        var drawPile2 = new DrawPile(new StrikeCard(), new StrikeCard());
        Assert.AreEqual(drawPile1, drawPile2);
    }

    [Test]
    public void TestEquality6()
    {
        var drawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
        var drawPile2 = new DrawPile(new StrikeCard(), new DefendCard());
        Assert.AreNotEqual(drawPile1, drawPile2);
    }

    [Test]
    public void TestEquality7()
    {
        var drawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
        var drawPile2 = new DrawPile(new StrikeCard(), new StrikeCard(), new DefendCard());
        Assert.AreNotEqual(drawPile1, drawPile2);
    }

    [Test]
    public void TestEquality8()
    {
        var drawPile1 = new DrawPile(new StrikeCard(), new DefendCard());
        var drawPile2 = new DrawPile(new StrikeCard(), new DefendCard());
        Assert.AreEqual(drawPile1, drawPile2);
    }

    [Test]
    public void EqualityIgnoresOrder()
    {
        var drawPile1 = new DrawPile(new DefendCard(), new StrikeCard());
        var drawPile2 = new DrawPile(new StrikeCard(), new DefendCard());
        Assert.AreEqual(drawPile1, drawPile2);
    }
}
