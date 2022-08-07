using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Values;

public readonly record struct NewHealth
{
    public int Current { get; }
    public int Maximum { get; }

    public NewHealth(int current, int maximum)
    {
        if (current > maximum) throw new ArgumentException("Current health cannot exceed maximum health");
        Current = current;
        Maximum = maximum;
    }

    public static NewHealth operator -(NewHealth newHealth, Damage damage)
    {
        return new NewHealth(newHealth.Current - damage.Amount, newHealth.Maximum);
    }

    public override string ToString()
    {
        return $"{Current}/{Maximum}";
    }
}

[TestFixture]
internal class NewHealthTests
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
    public void TestDamageSubtraction(int amountOfNewHealth, int amountOfDamage, int expectedAmountOfNewHealth)
    {
        Assert.AreEqual(new NewHealth(expectedAmountOfNewHealth, 10),
            new NewHealth(amountOfNewHealth, 10) - new Damage(amountOfDamage));
    }
}