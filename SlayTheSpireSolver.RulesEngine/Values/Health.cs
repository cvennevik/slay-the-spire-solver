﻿using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Values;

public readonly record struct Health(int Current)
{
    // TODO: Expand into current + max value

    public Health Damage(Damage damage)
    {
        return new Health(Current - damage.Amount);
    }

    public static Health operator -(Health health, Damage damage)
    {
        return new Health(health.Current - damage.Amount);
    }

    public static Health operator +(Health a, Health b)
    {
        return new Health(a.Current + b.Current);
    }

    public static bool operator <=(Health a, Health b)
    {
        return a.Current <= b.Current;
    }

    public static bool operator >=(Health a, Health b)
    {
        return a.Current >= b.Current;
    }

    public static bool operator <(Health a, Health b)
    {
        return a.Current < b.Current;
    }

    public static bool operator >(Health a, Health b)
    {
        return a.Current > b.Current;
    }

    public static implicit operator Health(int amount)
    {
        return new Health(amount);
    }

    public override string ToString()
    {
        return $"{Current}";
    }
}

[TestFixture]
internal class HealthTests
{
    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, 1, -1)]
    [TestCase(0, 2, -2)]
    [TestCase(1, 0, 1)]
    [TestCase(1, 1, 0)]
    [TestCase(1, 2, -1)]
    [TestCase(2, 0, 2)]
    [TestCase(2, 1, 1)]
    [TestCase(2, 2, 0)]
    public void TestDamageSubtraction(int amountOfHealth, int amountOfDamage, int expectedAmountOfHealth)
    {
        Assert.AreEqual(new Health(expectedAmountOfHealth),
            new Health(amountOfHealth).Damage(amountOfDamage));
    }

    [Test]
    [TestCase(-5)]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    public void ImplicitlyConvertedHealthEqualsExplicitHealth(int amount)
    {
        Health health = amount;
        Assert.AreEqual(new Health(amount), health);
        Assert.True(new Health(amount) == amount);
        Assert.True(amount == new Health(amount));
    }
}