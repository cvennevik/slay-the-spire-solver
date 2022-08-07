using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Values;

public readonly record struct NewHealth(int Current)
{
    public static NewHealth operator -(NewHealth newHealth, Damage damage)
    {
        return new NewHealth(newHealth.Current - damage.Amount);
    }

    public static NewHealth operator +(NewHealth a, NewHealth b)
    {
        return new NewHealth(a.Current + b.Current);
    }

    public override string ToString()
    {
        return $"{Current}";
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
        Assert.AreEqual(new NewHealth(expectedAmountOfNewHealth),
            new NewHealth(amountOfNewHealth) - new Damage(amountOfDamage));
    }
}