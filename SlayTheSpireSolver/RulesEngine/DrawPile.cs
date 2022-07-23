using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class DrawPile : CardCollection<DrawPile>
{
    public DrawPile() : this(Array.Empty<Card>())
    {
    }

    public DrawPile(params Card[] cards) : base(cards)
    {
    }

    public override DrawPile CreateNew(params Card[] cards)
    {
        return new DrawPile(cards);
    }
}

[TestFixture]
internal class DrawPileTests : CardCollectionTests<DrawPile>
{
    [TestFixture]
    internal class RemoveTests : DrawPileTests
    {
        [Test]
        public void ThrowsExceptionWhenDrawPileEmpty()
        {
            var drawPile = new DrawPile();
            Assert.Throws<ArgumentException>(() => drawPile.Remove(new Strike()));
        }

        [Test]
        public void ThrowsExceptionWhenCardNotInDrawPile()
        {
            var drawPile = new DrawPile(new Defend());
            Assert.Throws<ArgumentException>(() => drawPile.Remove(new Strike()));
        }

        [Test]
        public void RemovesStrike()
        {
            var drawPile = new DrawPile(new Strike());
            var newDrawPile = drawPile.Remove(new Strike());
            Assert.AreEqual(new DrawPile(), newDrawPile);
        }

        [Test]
        public void RemovesDefend()
        {
            var drawPile = new DrawPile(new Defend());
            var newDrawPile = drawPile.Remove(new Defend());
            Assert.AreEqual(new DrawPile(), newDrawPile);
        }

        [Test]
        public void RemovesOnlyOneCopy()
        {
            var drawPile = new DrawPile(new Defend(), new Defend());
            var newDrawPile = drawPile.Remove(new Defend());
            Assert.AreEqual(new DrawPile(new Defend()), newDrawPile);
        }
    }

    [TestFixture]
    internal class AddTests : DrawPileTests
    {
        [Test]
        public void AddsStrikeToEmptyDrawPile()
        {
            var drawPile = new DrawPile();
            var newDrawPile = drawPile.Add(new Strike());
            Assert.AreEqual(new DrawPile(new Strike()), newDrawPile);
        }

        [Test]
        public void AddsDefendToEmptyDrawPile()
        {
            var drawPile = new DrawPile();
            var newDrawPile = drawPile.Add(new Defend());
            Assert.AreEqual(new DrawPile(new Defend()), newDrawPile);
        }

        [Test]
        public void AddsStrikeToExistingDrawPile()
        {
            var drawPile = new DrawPile(new Strike());
            var newDrawPile = drawPile.Add(new Strike());
            Assert.AreEqual(new DrawPile(new Strike(), new Strike()), newDrawPile);
        }

        [Test]
        public void AddsDefendToExistingDrawPile()
        {
            var drawPile = new DrawPile(new Strike());
            var newDrawPile = drawPile.Add(new Defend());
            Assert.AreEqual(new DrawPile(new Strike(), new Defend()), newDrawPile);
        }
    }
}