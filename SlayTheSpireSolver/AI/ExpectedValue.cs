namespace SlayTheSpireSolver.AI;

public record ExpectedValue : IComparable<ExpectedValue>
{
    public ExpectedValue(double minimum, double estimate, double maximum)
    {
        const double tolerance = 0.0000000000001;
        if ((minimum > estimate && minimum - estimate > tolerance) ||
            (estimate > maximum && estimate - maximum > tolerance))
            throw new ArgumentException(
                $"Illegal estimate: Minimum = {minimum}, Estimate = {estimate}, Maximum = {maximum}");
        Minimum = minimum;
        Estimate = estimate;
        Maximum = maximum;
    }

    public ExpectedValue(double minimum, double maximum) : this(minimum, minimum, maximum)
    {
    }

    public ExpectedValue(double estimate) : this(estimate, estimate, estimate)
    {
    }

    public double Estimate { get; }
    public double Minimum { get; }
    public double Maximum { get; }

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
        return $"{{Estimate: {Estimate}, Minimum: {Minimum}, Maximum: {Maximum}}}";
    }
}