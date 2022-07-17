namespace SlayTheSpireSolver.AI;

public record Range(double Minimum, double Maximum)
{
    public static Range operator +(Range a, Range b)
    {
        return new Range(Math.Min(a.Minimum, b.Minimum), Math.Max(a.Maximum, b.Maximum));
    }
}