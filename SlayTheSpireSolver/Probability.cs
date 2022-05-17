namespace SlayTheSpireSolver;

public record Probability
{
    public double Value { get; }

    public Probability(double value)
    {
        if (value > 1 || value < 0) throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;
    }
}
