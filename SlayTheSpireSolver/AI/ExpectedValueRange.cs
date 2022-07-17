using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.AI;

public record ValueRange
{
    public ValueRange(double Minimum, double Maximum)
    {
        this.Minimum = Minimum;
        this.Maximum = Maximum;
    }

    public double ToExpectedValue => Minimum;
    public double Minimum { get; init; }
    public double Maximum { get; init; }

    public static ValueRange operator +(ValueRange a, ValueRange b)
    {
        return new ValueRange(a.Minimum + b.Minimum, a.Minimum + b.Maximum);
    }

    public static ValueRange operator *(ValueRange range, Probability probability)
    {
        return new ValueRange(range.Minimum * probability.Value, range.Maximum * probability.Value);
    }

    public bool StrictlyBetterThan(ValueRange otherRange)
    {
        return Minimum > otherRange.Maximum;
    }
}