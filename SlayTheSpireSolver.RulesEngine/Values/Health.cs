﻿using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Values;

public readonly record struct Health
{

    public int Current { get; init; }

    public Health(int Current)
    {
        this.Current = Current;
    }

    // TODO: Expand into current + max value

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
            new Health(amountOfHealth) - new Damage(amountOfDamage));
    }
}