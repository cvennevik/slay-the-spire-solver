using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.AI;

public record ValueRange(double Minimum, double Maximum)
{
    public double ToExpectedValue => Minimum;

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