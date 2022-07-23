using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Buffs;

public record Strength
{
    public int Amount { get; }

    public Strength(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }

    public static bool operator >(Strength a, Strength b) => a.Amount > b.Amount;
    public static bool operator >=(Strength a, Strength b) => a.Amount >= b.Amount;
    public static bool operator <(Strength a, Strength b) => a.Amount < b.Amount;
    public static bool operator <=(Strength a, Strength b) => a.Amount <= b.Amount;
    public static Strength operator +(Strength a, Strength b) => new(a.Amount + b.Amount);
    public static Strength operator -(Strength a, Strength b) => new(a < b ? 0 : a.Amount - b.Amount);

    public static implicit operator Strength(int amount) => amount > 0 ? new Strength(amount) : new Strength(0);

    public override string ToString()
    {
        return $"{Amount}";
    }
}

[TestFixture]
internal class StrengthTests
{
    [Test]
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-999)]
    public void AmountCannotBeNegative(int amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Strength(amount));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(999)]
    public void StrengthWithEqualAmountsAreEqual(int amount)
    {
        Assert.AreEqual(new Strength(amount), new Strength(amount));
    }

    [Test]
    [TestCase(0, 0, false)]
    [TestCase(0, 1, false)]
    [TestCase(0, 2, false)]
    [TestCase(1, 0, true)]
    [TestCase(1, 1, false)]
    [TestCase(1, 2, false)]
    [TestCase(2, 0, true)]
    [TestCase(2, 1, true)]
    [TestCase(2, 2, false)]
    public void TestGreaterThan(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Strength(amountA) > new Strength(amountB));
    }

    [Test]
    [TestCase(0, 0, true)]
    [TestCase(0, 1, false)]
    [TestCase(0, 2, false)]
    [TestCase(1, 0, true)]
    [TestCase(1, 1, true)]
    [TestCase(1, 2, false)]
    [TestCase(2, 0, true)]
    [TestCase(2, 1, true)]
    [TestCase(2, 2, true)]
    public void TestGreaterThanOrEqualTo(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Strength(amountA) >= new Strength(amountB));
    }

    [Test]
    [TestCase(0, 0, false)]
    [TestCase(0, 1, true)]
    [TestCase(0, 2, true)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, false)]
    [TestCase(1, 2, true)]
    [TestCase(2, 0, false)]
    [TestCase(2, 1, false)]
    [TestCase(2, 2, false)]
    public void TestLessThan(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Strength(amountA) < new Strength(amountB));
    }

    [Test]
    [TestCase(0, 0, true)]
    [TestCase(0, 1, true)]
    [TestCase(0, 2, true)]
    [TestCase(1, 0, false)]
    [TestCase(1, 1, true)]
    [TestCase(1, 2, true)]
    [TestCase(2, 0, false)]
    [TestCase(2, 1, false)]
    [TestCase(2, 2, true)]
    public void TestLessThanOrEqualTo(int amountA, int amountB, bool expectedResult)
    {
        Assert.AreEqual(expectedResult, new Strength(amountA) <= new Strength(amountB));
    }

    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, 1, 1)]
    [TestCase(0, 2, 2)]
    [TestCase(1, 0, 1)]
    [TestCase(1, 1, 2)]
    [TestCase(1, 2, 3)]
    [TestCase(2, 0, 2)]
    [TestCase(2, 1, 3)]
    [TestCase(2, 2, 4)]
    public void TestPlus(int amountA, int amountB, int expectedAmount)
    {
        Assert.AreEqual(new Strength(expectedAmount), new Strength(amountA) + new Strength(amountB));
    }

    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, 1, 0)]
    [TestCase(0, 2, 0)]
    [TestCase(1, 0, 1)]
    [TestCase(1, 1, 0)]
    [TestCase(1, 2, 0)]
    [TestCase(2, 0, 2)]
    [TestCase(2, 1, 1)]
    [TestCase(2, 2, 0)]
    public void TestMinus(int amountA, int amountB, int expectedAmount)
    {
        Assert.AreEqual(new Strength(expectedAmount), new Strength(amountA) - new Strength(amountB));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    public void ImplicitlyConvertedStrengthEqualsExplicitStrength(int amount)
    {
        Strength strength = amount;
        Assert.AreEqual(new Strength(amount), strength);
        Assert.True(new Strength(amount) == amount);
        Assert.True(amount == new Strength(amount));
    }

    [Test]
    public void ImplicitlyConvertsNegativeAmountsToZeroStrength()
    {
        Assert.True(new Strength(0) == 0);
        Assert.True(new Strength(0) == -1);
        Assert.True(new Strength(0) == -5);
    }
}