using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.AI;

public record ExpectedValue : IComparable<ExpectedValue>
{
    public ExpectedValue(Range range)
    {
        Range = range;
        Estimate = range.Minimum;
    }

    public ExpectedValue(double minimum, double maximum)
    {
        Range = new Range(minimum, maximum);
        Estimate = minimum;
    }

    public ExpectedValue(double estimate)
    {
        Estimate = estimate;
        Range = new Range(estimate, estimate);
    }

    public ExpectedValue(double estimate, Range range)
    {
        Estimate = estimate;
        Range = range;
    }

    public Range Range { get; init; }
    public double Estimate { get; init; }

    public int CompareTo(ExpectedValue? other)
    {
        if (other == null) return 1;

        var difference = Estimate - other.Estimate;
        return difference switch
        {
            < 0 => -1,
            0 => 0,
            _ => 1
        };
    }

    public static ExpectedValue operator +(ExpectedValue a, ExpectedValue b)
    {
        return new ExpectedValue(a.Range.Minimum + b.Range.Minimum, a.Range.Minimum + b.Range.Maximum);
    }

    public static ExpectedValue operator *(ExpectedValue expectedValue, Probability probability)
    {
        return new ExpectedValue(expectedValue.Range.Minimum * probability.Value,
            expectedValue.Range.Maximum * probability.Value);
    }
}