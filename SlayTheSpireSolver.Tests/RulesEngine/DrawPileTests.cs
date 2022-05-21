﻿using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;

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
            var drawPile2 = new DrawPile(new StrikeCard());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test3()
        {
            var drawPile1 = new DrawPile(new StrikeCard());
            var drawPile2 = new DrawPile(new StrikeCard());
            Assert.AreEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test4()
        {
            var drawPile1 = new DrawPile(new StrikeCard());
            var drawPile2 = new DrawPile(new StrikeCard(), new StrikeCard());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test5()
        {
            var drawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
            var drawPile2 = new DrawPile(new StrikeCard(), new StrikeCard());
            Assert.AreEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test6()
        {
            var drawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
            var drawPile2 = new DrawPile(new StrikeCard(), new DefendCard());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test7()
        {
            var drawPile1 = new DrawPile(new StrikeCard(), new StrikeCard());
            var drawPile2 = new DrawPile(new StrikeCard(), new StrikeCard(), new DefendCard());
            Assert.AreNotEqual(drawPile1, drawPile2);
        }

        [Test]
        public void Test8()
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
}