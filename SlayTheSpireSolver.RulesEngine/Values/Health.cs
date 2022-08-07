using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Values;

public readonly record struct Health
{
    // TODO: Expand into current + max value
    public int Current { get; }
    private int Maximum { get; }

    public Health(int current, int maximum = int.MaxValue)
    {
        if (current > maximum) throw new ArgumentException("Current health cannot exceed maximum health");
        Current = current;
        Maximum = maximum;
    }

    public Health Heal(Healing healing)
    {
        return new Health(Current + healing.Amount, Maximum);
    }

    public static Health operator -(Health health, Damage damage)
    {
        return new Health(health.Current - damage.Amount, health.Maximum);
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
        return $"{Current}/{Maximum}";
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
        Assert.AreEqual(new Health(expectedAmountOfHealth, 100),
            new Health(amountOfHealth, 100) - new Damage(amountOfDamage));
    }

    [Test]
    public void TestHeal()
    {
        var healing = new Healing(5);
        var health = new Health(10, 30);
        Assert.AreEqual(new Health(15, 30), health.Heal(healing));
    }
}