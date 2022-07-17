using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.AI;

public record ExpectedValue(double Minimum, double Maximum) : IComparable<ExpectedValue>
{
    public Range Range { get; init; } = new(Minimum, Maximum);
    public double Minimum { get; init; } = Minimum;
    public double Maximum { get; init; } = Maximum;
    public double BestEstimate { get; init; } = Minimum;

    public int CompareTo(ExpectedValue? other)
    {
        if (other == null) return 1;

        var difference = BestEstimate - other.BestEstimate;
        return difference switch
        {
            < 0 => -1,
            0 => 0,
            _ => 1
        };
    }

    public static ExpectedValue operator +(ExpectedValue a, ExpectedValue b)
    {
        return new ExpectedValue(a.Minimum + b.Minimum, a.Minimum + b.Maximum);
    }

    public static ExpectedValue operator *(ExpectedValue range, Probability probability)
    {
        return new ExpectedValue(range.Minimum * probability.Value, range.Maximum * probability.Value);
    }
}