using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class DrawPileTests
{
    [TestFixture]
    public class EqualityTests : DrawPileTests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual(new DrawPile(), new DrawPile());
        }

        [Test]
        public void Test2()
        {
            var drawPile1 = new DrawPile();
            var drawPile2 = new DrawPile(new Strike());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test3()
        {
            var drawPile1 = new DrawPile(new Strike());
            var drawPile2 = new DrawPile(new Strike());
            Assert.AreEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test4()
        {
            var drawPile1 = new DrawPile(new Strike());
            var drawPile2 = new DrawPile(new Strike(), new Strike());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test5()
        {
            var drawPile1 = new DrawPile(new Strike(), new Strike());
            var drawPile2 = new DrawPile(new Strike(), new Strike());
            Assert.AreEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test6()
        {
            var drawPile1 = new DrawPile(new Strike(), new Strike());
            var drawPile2 = new DrawPile(new Strike(), new Defend());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test7()
        {
            var drawPile1 = new DrawPile(new Strike(), new Strike());
            var drawPile2 = new DrawPile(new Strike(), new Strike(), new Defend());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test8()
        {
            var drawPile1 = new DrawPile(new Strike(), new Defend());
            var drawPile2 = new DrawPile(new Strike(), new Defend());
            Assert.AreEqual(drawPile1, drawPile2);
        }

        [Test]
        public void EqualityIgnoresOrder()
        {
            var drawPile1 = new DrawPile(new Defend(), new Strike());
            var drawPile2 = new DrawPile(new Strike(), new Defend());
            Assert.AreEqual(drawPile1, drawPile2);
        }
    }

    [TestFixture]
    public class RemoveTests : DrawPileTests
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
    public class AddTests : DrawPileTests
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