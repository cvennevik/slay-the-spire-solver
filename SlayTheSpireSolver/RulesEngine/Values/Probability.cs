namespace SlayTheSpireSolver.RulesEngine.Values;

public record Probability
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Probability(int numerator, int denominator)
    {
        if (numerator > denominator) throw new ArgumentException("Numerator cannot be larger than denominator");
        Numerator = numerator;
        Denominator = denominator;
    }
}