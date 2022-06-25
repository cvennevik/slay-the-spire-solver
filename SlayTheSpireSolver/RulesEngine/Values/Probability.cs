namespace SlayTheSpireSolver.RulesEngine.Values;

public record Probability
{
    public double Value { get; }

    public Probability(double value)
    {
        if (value is > 1 or < 0)
        {
            throw new ArgumentException("Probability value must be between 1 and 0", nameof(value));
        }

        Value = value;
    }
}