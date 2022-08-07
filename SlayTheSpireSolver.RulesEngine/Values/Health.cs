using NUnit.Framework;

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

    public Health Heal(int healing)
    {
        if (healing < 0) throw new ArgumentException("Cannot heal negative amount");
        return new Health(Math.Min(Current + healing, Maximum), Maximum);
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
        Assert.Throws<ArgumentException>(() => new Health(10).Heal(-1));
        Assert.AreEqual(new Health(10, 30), new Health(10, 30).Heal(0));
        Assert.AreEqual(new Health(15, 30), new Health(10, 30).Heal(5));
        Assert.AreEqual(new Health(30, 30), new Health(10, 30).Heal(100));
    }
}