using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.AI;

public record ExpectedValueRange
{
    public ExpectedValueRange(double Minimum, double Maximum)
    {
        this.Minimum = Minimum;
        this.Maximum = Maximum;
    }

    public double ToExpectedValue => Minimum;
    public double Minimum { get; init; }
    public double Maximum { get; init; }

    public static ExpectedValueRange operator +(ExpectedValueRange a, ExpectedValueRange b) =>
        new(a.Minimum + b.Minimum, a.Minimum + b.Maximum);

    public static ExpectedValueRange operator *(ExpectedValueRange range, Probability probability) =>
        new(range.Minimum * probability.Value, range.Maximum * probability.Value);
}
