using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Values;

public record Probability
{
    public Probability(double value)
    {
        if (value is > 1 or < 0)
            throw new ArgumentException("Probability value must be between 1 and 0", nameof(value));

        Value = value;
    }

    public double Value { get; }

    public Probability Add(Probability other) => new(Value + other.Value);

    public bool IsEqualTo(Probability other, double tolerance = double.Epsilon) =>
        Math.Abs(Value - other.Value) < tolerance;

    public static double operator -(double number, Probability probability) => number - probability.Value;
    public static Probability operator *(Probability a, Probability b) => new(a.Value * b.Value);

    public static implicit operator Probability(double value) =>
        value switch
        {
            < 0 => new Probability(0),
            > 1 => new Probability(1),
            _ => new Probability(value)
        };
}

[TestFixture]
internal class ProbabilityTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new Probability(1), new Probability(1));
        Assert.AreEqual(new Probability(0), new Probability(0));
        Assert.AreEqual(new Probability(0.5), new Probability(0.5));
        Assert.AreNotEqual(new Probability(1), new Probability(0));
    }

    [Test]
    public void ThrowsExceptionForValueAboveOne() => Assert.Throws<ArgumentException>(() => new Probability(1.01));

    [Test]
    public void ThrowsExceptionForValueBelowZero() => Assert.Throws<ArgumentException>(() => new Probability(-0.01));
}
