using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.AI;

public record ExpectedValue(double Minimum, double Maximum)
{
    public double Estimate => Minimum;

    public static ExpectedValue operator +(ExpectedValue a, ExpectedValue b)
    {
        return new ExpectedValue(a.Minimum + b.Minimum, a.Minimum + b.Maximum);
    }

    public static ExpectedValue operator *(ExpectedValue range, Probability probability)
    {
        return new ExpectedValue(range.Minimum * probability.Value, range.Maximum * probability.Value);
    }

    public bool StrictlyBetterThan(ExpectedValue other)
    {
        return Minimum > other.Maximum;
    }
}