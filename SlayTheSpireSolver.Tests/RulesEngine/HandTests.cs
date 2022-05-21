using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards.Defend;
using SlayTheSpireSolver.RulesEngine.Cards.Strike;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class HandTests
{
    [TestFixture]
    public class EqualityTests : HandTests
    {
        [Test]
            public void Test1()
            {
                Assert.AreEqual(new Hand(), new Hand());
            }
        
            [Test]
            public void Test2()
            {
                var hand1 = new Hand();
                var hand2 = new Hand(new StrikeCard());
                Assert.AreNotEqual(hand1, hand2);
            }
        
            [Test]
            public void Test3()
            {
                var hand1 = new Hand(new StrikeCard());
                var hand2 = new Hand(new StrikeCard());
                Assert.AreEqual(hand1, hand2);
            }
        
            [Test]
            public void Test4()
            {
                var hand1 = new Hand(new StrikeCard());
                var hand2 = new Hand(new StrikeCard(), new StrikeCard());
                Assert.AreNotEqual(hand1, hand2);
            }
        
            [Test]
            public void Test5()
            {
                var hand1 = new Hand(new StrikeCard(), new StrikeCard());
                var hand2 = new Hand(new StrikeCard(), new StrikeCard());
                Assert.AreEqual(hand1, hand2);
            }
        
            [Test]
            public void Test6()
            {
                var hand1 = new Hand(new StrikeCard(), new StrikeCard());
                var hand2 = new Hand(new StrikeCard(), new DefendCard());
                Assert.AreNotEqual(hand1, hand2);
            }
        
            [Test]
            public void Test7()
            {
                var hand1 = new Hand(new StrikeCard(), new StrikeCard());
                var hand2 = new Hand(new StrikeCard(), new StrikeCard(), new DefendCard());
                Assert.AreNotEqual(hand1, hand2);
            }
        
            [Test]
            public void Test8()
            {
                var hand1 = new Hand(new StrikeCard(), new DefendCard());
                var hand2 = new Hand(new StrikeCard(), new DefendCard());
                Assert.AreEqual(hand1, hand2);
            }
        
            [Test]
            public void EqualityIgnoresOrder()
            {
                var hand1 = new Hand(new DefendCard(), new StrikeCard());
                var hand2 = new Hand(new StrikeCard(), new DefendCard());
                Assert.AreEqual(hand1, hand2);
            }
    }

    [TestFixture]
    public class AddTests : HandTests
    {
        [Test]
        public void AddsStrikeToEmptyHand()
        {
            var hand = new Hand();
            var newHand = hand.Add(new StrikeCard());
            Assert.AreEqual(new Hand(new StrikeCard()), newHand);
        }

        [Test]
        public void AddsDefendToEmptyHand()
        {
            var hand = new Hand();
            var newHand = hand.Add(new DefendCard());
            Assert.AreEqual(new Hand(new DefendCard()), newHand);
        }

        [Test]
        public void AddsStrikeToExistingHand()
        {
            var hand = new Hand(new StrikeCard());
            var newHand = hand.Add(new StrikeCard());
            Assert.AreEqual(new Hand(new StrikeCard(), new StrikeCard()), newHand);
        }

        [Test]
        public void AddsDefendToExistingHand()
        {
            var hand = new Hand(new StrikeCard());
            var newHand = hand.Add(new DefendCard());
            Assert.AreEqual(new Hand(new StrikeCard(), new DefendCard()), newHand);
        }
    }

    [TestFixture]
    public class RemoveTests : HandTests
    {
        [Test]
        public void ThrowsExceptionWhenHandEmpty()
        {
            var hand = new Hand();
            Assert.Throws<ArgumentException>(() => hand.Remove(new StrikeCard()));
        }

        [Test]
        public void RemoveStrikeCard()
        {
            var hand = new Hand(new StrikeCard());
            var newHand = hand.Remove(new StrikeCard());
            Assert.AreEqual(new Hand(), newHand);
        }

        [Test]
        public void RemovesDefendCard()
        {
            var hand = new Hand(new DefendCard());
            var newHand = hand.Remove(new DefendCard());
            Assert.AreEqual(new Hand(), newHand);
        }

        [Test]
        public void RemovesOnlyOneCopy()
        {
            var hand = new Hand(new StrikeCard(), new StrikeCard());
            var newHand = hand.Remove(new StrikeCard());
            Assert.AreEqual(new Hand(new StrikeCard()), newHand);
        }

        [Test]
        public void RemovesCorrectCardType()
        {
            var hand = new Hand(new StrikeCard(), new DefendCard());
            var newHand = hand.Remove(new StrikeCard());
            Assert.AreEqual(new Hand(new DefendCard()), newHand);
        }

        [Test]
        public void ThrowsExceptionWhenCardTypeNotInHand()
        {
            var hand = new Hand(new DefendCard());
            Assert.Throws<ArgumentException>(() => hand.Remove(new StrikeCard()));
        }
    }

    [TestFixture]
    public class ContainsTests : HandTests
    {
        [Test]
        public void TestSingleDefend()
        {
            var hand = new Hand(new DefendCard());
            Assert.True(hand.Contains(new DefendCard()));
            Assert.False(hand.Contains(new StrikeCard()));
        }

        [Test]
        public void TestStrikeAndDefend()
        {
            var hand = new Hand(new DefendCard(), new StrikeCard());
            Assert.True(hand.Contains(new DefendCard()));
            Assert.True(hand.Contains(new StrikeCard()));
        }

        [Test]
        public void TestEmptyHand()
        {
            var hand = new Hand();
            Assert.False(hand.Contains(new DefendCard()));
            Assert.False(hand.Contains(new StrikeCard()));
        }

        [Test]
        public void TestTwoDefends()
        {
            var hand = new Hand(new DefendCard(), new DefendCard());
            Assert.True(hand.Contains(new DefendCard()));
            Assert.False(hand.Contains(new StrikeCard()));
        }
    }
}
