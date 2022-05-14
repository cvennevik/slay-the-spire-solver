﻿using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Cards.Defend;
using System;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class HandTests
{
    [Test]
    public void TestEquality1()
    {
        Assert.AreEqual(new Hand(), new Hand());
    }

    [Test]
    public void TestEquality2()
    {
        var hand1 = new Hand();
        var hand2 = new Hand(new StrikeCard());
        Assert.AreNotEqual(hand1, hand2);
    }

    [Test]
    public void TestEquality3()
    {
        var hand1 = new Hand(new StrikeCard());
        var hand2 = new Hand(new StrikeCard());
        Assert.AreEqual(hand1, hand2);
    }

    [Test]
    public void TestEquality4()
    {
        var hand1 = new Hand(new StrikeCard());
        var hand2 = new Hand(new StrikeCard(), new StrikeCard());
        Assert.AreNotEqual(hand1, hand2);
    }

    [Test]
    public void TestEquality5()
    {
        var hand1 = new Hand(new StrikeCard(), new StrikeCard());
        var hand2 = new Hand(new StrikeCard(), new StrikeCard());
        Assert.AreEqual(hand1, hand2);
    }

    [Test]
    public void TestRemove1()
    {
        var hand = new Hand();
        Assert.Throws<ArgumentException>(() => hand.Remove(new StrikeCard()));
    }

    [Test]
    public void TestRemove2()
    {
        var hand = new Hand(new StrikeCard());
        var newHand = hand.Remove(new StrikeCard());
        Assert.AreEqual(new Hand(), newHand);
    }

    [Test]
    public void TestRemove3()
    {
        var hand = new Hand(new StrikeCard(), new StrikeCard());
        var newHand = hand.Remove(new StrikeCard());
        Assert.AreEqual(new Hand(new StrikeCard()), newHand);
    }

    [Test]
    public void TestRemove4()
    {
        var hand = new Hand(new StrikeCard(), new DefendCard());
        var newHand = hand.Remove(new StrikeCard());
        Assert.AreEqual(new Hand(new DefendCard()), newHand);
    }

    [Test]
    public void TestRemove5()
    {
        var hand = new Hand(new DefendCard());
        Assert.Throws<ArgumentException>(() => hand.Remove(new StrikeCard()));
    }

    [Test]
    public void TestRemove6()
    {
        var hand = new Hand(new DefendCard());
        var newHand = hand.Remove(new DefendCard());
        Assert.AreEqual(new Hand(), newHand);
    }
}
