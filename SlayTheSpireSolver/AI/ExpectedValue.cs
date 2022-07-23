namespace SlayTheSpireSolver.AI;

public record ExpectedValue
{
    public ExpectedValue(double exactExpectedValue) : this(exactExpectedValue, exactExpectedValue)
    {
    }

    public ExpectedValue(double minimum, double estimate)
    {
        const double tolerance = 0.0000000000001;
        if (minimum > estimate && minimum - estimate > tolerance)
            throw new ArgumentException($"Illegal expected value: Minimum = {minimum}, Estimate = {estimate}");
        Minimum = minimum;
        Estimate = estimate;
    }

    public double Minimum { get; }
    public double Estimate { get; }

    public override string ToString()
    {
        return $"{{Minimum: {Minimum}, Estimate: {Estimate}}}";
    }
}