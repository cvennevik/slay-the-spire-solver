namespace SlayTheSpireSolver.AI;

public record ExpectedValueRange(double Minimum, double Maximum)
{
    public double ToExpectedValue => Minimum;
}
