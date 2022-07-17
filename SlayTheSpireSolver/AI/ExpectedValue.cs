using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.AI;

public record ExpectedValue : IComparable<ExpectedValue>
{
    public ExpectedValue(double Minimum, double Maximum)
    {
        Range = new Range(Minimum, Maximum);
        this.Minimum = Minimum;
        this.Maximum = Maximum;
        Estimate = Minimum;
    }

    public ExpectedValue(double estimate)
    {
        Estimate = estimate;
        Range = new Range(estimate, estimate);
    }

    public Range Range { get; init; }
    public double Minimum { get; init; }
    public double Maximum { get; init; }
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

    public static ExpectedValue operator *(ExpectedValue range, Probability probability)
    {
        return new ExpectedValue(range.Minimum * probability.Value, range.Maximum * probability.Value);
    }

    public void Deconstruct(out double Minimum, out double Maximum)
    {
        Minimum = this.Minimum;
        Maximum = this.Maximum;
    }
}