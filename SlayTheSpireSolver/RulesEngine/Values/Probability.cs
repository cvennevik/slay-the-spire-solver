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

    public static Probability operator *(Probability a, Probability b) => new Probability(a.Value * b.Value);

    public static implicit operator Probability(double value)
    {
        return value switch
        {
            < 0 => new Probability(0),
            > 1 => new Probability(1),
            _ => new Probability(value)
        };
    }
}