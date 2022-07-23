using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine;

public class Hand : CardCollection<Hand>
{
    public Hand() : this(Array.Empty<Card>())
    {
    }

    public Hand(params Card[] cards) : base(cards)
    {
    }

    public override Hand CreateNew(params Card[] cards)
    {
        return new Hand(cards);
    }
}

[TestFixture]
internal class HandTests
{
    [TestFixture]
    internal class EqualityTests : HandTests
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
            var hand2 = new Hand(new Strike());
            Assert.AreNotEqual(hand1, hand2);
        }

        [Test]
        public void Test3()
        {
            var hand1 = new Hand(new Strike());
            var hand2 = new Hand(new Strike());
            Assert.AreEqual(hand1, hand2);
        }

        [Test]
        public void Test4()
        {
            var hand1 = new Hand(new Strike());
            var hand2 = new Hand(new Strike(), new Strike());
            Assert.AreNotEqual(hand1, hand2);
        }

        [Test]
        public void Test5()
        {
            var hand1 = new Hand(new Strike(), new Strike());
            var hand2 = new Hand(new Strike(), new Strike());
            Assert.AreEqual(hand1, hand2);
        }

        [Test]
        public void Test6()
        {
            var hand1 = new Hand(new Strike(), new Strike());
            var hand2 = new Hand(new Strike(), new Defend());
            Assert.AreNotEqual(hand1, hand2);
        }

        [Test]
        public void Test7()
        {
            var hand1 = new Hand(new Strike(), new Strike());
            var hand2 = new Hand(new Strike(), new Strike(), new Defend());
            Assert.AreNotEqual(hand1, hand2);
        }

        [Test]
        public void Test8()
        {
            var hand1 = new Hand(new Strike(), new Defend());
            var hand2 = new Hand(new Strike(), new Defend());
            Assert.AreEqual(hand1, hand2);
        }

        [Test]
        public void EqualityIgnoresOrder()
        {
            var hand1 = new Hand(new Defend(), new Strike());
            var hand2 = new Hand(new Strike(), new Defend());
            Assert.AreEqual(hand1, hand2);
        }
    }

    [TestFixture]
    internal class AddTests : HandTests
    {
        [Test]
        public void AddsStrikeToEmptyHand()
        {
            var hand = new Hand();
            var newHand = hand.Add(new Strike());
            Assert.AreEqual(new Hand(new Strike()), newHand);
        }

        [Test]
        public void AddsDefendToEmptyHand()
        {
            var hand = new Hand();
            var newHand = hand.Add(new Defend());
            Assert.AreEqual(new Hand(new Defend()), newHand);
        }

        [Test]
        public void AddsStrikeToExistingHand()
        {
            var hand = new Hand(new Strike());
            var newHand = hand.Add(new Strike());
            Assert.AreEqual(new Hand(new Strike(), new Strike()), newHand);
        }

        [Test]
        public void AddsDefendToExistingHand()
        {
            var hand = new Hand(new Strike());
            var newHand = hand.Add(new Defend());
            Assert.AreEqual(new Hand(new Strike(), new Defend()), newHand);
        }
    }

    [TestFixture]
    internal class RemoveTests : HandTests
    {
        [Test]
        public void ThrowsExceptionWhenHandEmpty()
        {
            var hand = new Hand();
            Assert.Throws<ArgumentException>(() => hand.Remove(new Strike()));
        }

        [Test]
        public void RemoveStrikeCard()
        {
            var hand = new Hand(new Strike());
            var newHand = hand.Remove(new Strike());
            Assert.AreEqual(new Hand(), newHand);
        }

        [Test]
        public void RemovesDefendCard()
        {
            var hand = new Hand(new Defend());
            var newHand = hand.Remove(new Defend());
            Assert.AreEqual(new Hand(), newHand);
        }

        [Test]
        public void RemovesOnlyOneCopy()
        {
            var hand = new Hand(new Strike(), new Strike());
            var newHand = hand.Remove(new Strike());
            Assert.AreEqual(new Hand(new Strike()), newHand);
        }

        [Test]
        public void RemovesCorrectCardType()
        {
            var hand = new Hand(new Strike(), new Defend());
            var newHand = hand.Remove(new Strike());
            Assert.AreEqual(new Hand(new Defend()), newHand);
        }

        [Test]
        public void ThrowsExceptionWhenCardTypeNotInHand()
        {
            var hand = new Hand(new Defend());
            Assert.Throws<ArgumentException>(() => hand.Remove(new Strike()));
        }
    }

    [TestFixture]
    internal class ContainsTests : HandTests
    {
        [Test]
        public void TestSingleDefend()
        {
            var hand = new Hand(new Defend());
            Assert.True(hand.Contains(new Defend()));
            Assert.False(hand.Contains(new Strike()));
        }

        [Test]
        public void TestStrikeAndDefend()
        {
            var hand = new Hand(new Defend(), new Strike());
            Assert.True(hand.Contains(new Defend()));
            Assert.True(hand.Contains(new Strike()));
        }

        [Test]
        public void TestEmptyHand()
        {
            var hand = new Hand();
            Assert.False(hand.Contains(new Defend()));
            Assert.False(hand.Contains(new Strike()));
        }

        [Test]
        public void TestTwoDefends()
        {
            var hand = new Hand(new Defend(), new Defend());
            Assert.True(hand.Contains(new Defend()));
            Assert.False(hand.Contains(new Strike()));
        }
    }
}