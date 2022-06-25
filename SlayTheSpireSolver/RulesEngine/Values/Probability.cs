namespace SlayTheSpireSolver.RulesEngine.Values;

public record Probability
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Probability(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }
}