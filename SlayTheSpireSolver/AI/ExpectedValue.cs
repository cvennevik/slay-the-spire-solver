namespace SlayTheSpireSolver.AI;

public record ExpectedValue : IComparable<ExpectedValue>
{
    public ExpectedValue(double minimum, double estimate)
    {
        const double tolerance = 0.0000000000001;
        if (minimum > estimate && minimum - estimate > tolerance)
            throw new ArgumentException($"Illegal expected value: Minimum = {minimum}, Estimate = {estimate}");
        Minimum = minimum;
        Estimate = estimate;
    }

    public ExpectedValue(double estimate) : this(estimate, estimate)
    {
    }

    public double Estimate { get; }
    public double Minimum { get; }

    public int CompareTo(ExpectedValue? other)
    {
        if (other == null) return 1;

        var difference = Estimate - other.Estimate;
        return difference switch
        {
            < 0 => -1,
            0 => 0,
            _ => 1
        };
    }

    public override string ToString()
    {
        return $"{{Estimate: {Estimate}, Minimum: {Minimum}}}";
    }
}