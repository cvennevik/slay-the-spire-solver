namespace SlayTheSpireSolver.RulesEngine.Values;

public record Probability
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Probability(int numerator, int denominator)
    {
        if (numerator > denominator) throw new ArgumentException("Numerator cannot be larger than denominator");
        if (numerator < 0) throw new ArgumentException("Numerator must be non-negative");
        if (denominator < 1) throw new ArgumentException("Denominator must be positive");
        Numerator = numerator;
        Denominator = denominator;
    }

    public static Probability operator +(Probability a, Probability b) =>
        new(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
}