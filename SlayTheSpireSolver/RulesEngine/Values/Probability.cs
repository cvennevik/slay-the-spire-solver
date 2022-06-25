namespace SlayTheSpireSolver.RulesEngine.Values;

public record Probability
{
    public double Value { get; }

    public Probability(double value)
    {
        if (value is > 1 or <= 0) throw new ArgumentException(nameof(value));
        Value = value;
    }
}