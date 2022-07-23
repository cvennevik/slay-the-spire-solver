using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DiscardPile : CardCollection<DiscardPile>
{
    public DiscardPile() : this(Array.Empty<Card>())
    {
    }

    public DiscardPile(params Card[] cards) : base(cards)
    {
    }

    public override DiscardPile CreateNew(params Card[] cards)
    {
        return new DiscardPile(cards);
    }
}

[TestFixture]
internal class DiscardPileTests : CardCollectionTests<DiscardPile>
{
    [Test]
    public void TestEquality2()
    {
        var discardPile1 = new DiscardPile();
        var discardPile2 = new DiscardPile(new Strike());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality3()
    {
        var discardPile1 = new DiscardPile(new Strike());
        var discardPile2 = new DiscardPile(new Strike());
        Assert.AreEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality4()
    {
        var discardPile1 = new DiscardPile(new Strike());
        var discardPile2 = new DiscardPile(new Strike(), new Strike());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality5()
    {
        var discardPile1 = new DiscardPile(new Strike(), new Strike());
        var discardPile2 = new DiscardPile(new Strike(), new Strike());
        Assert.AreEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality6()
    {
        var discardPile1 = new DiscardPile(new Strike(), new Strike());
        var discardPile2 = new DiscardPile(new Strike(), new Defend());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality7()
    {
        var discardPile1 = new DiscardPile(new Strike(), new Strike());
        var discardPile2 = new DiscardPile(new Strike(), new Strike(), new Defend());
        Assert.AreNotEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality8()
    {
        var discardPile1 = new DiscardPile(new Strike(), new Defend());
        var discardPile2 = new DiscardPile(new Strike(), new Defend());
        Assert.AreEqual(discardPile1, discardPile2);
    }

    [Test]
    public void TestEquality9()
    {
        var discardPile1 = new DiscardPile(new Defend(), new Strike());
        var discardPile2 = new DiscardPile(new Strike(), new Defend());
        Assert.AreEqual(discardPile1, discardPile2);
    }
}