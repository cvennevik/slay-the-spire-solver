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
internal class HandTests : CardCollectionTests<Hand>
{
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
}