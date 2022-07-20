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
        Range = new Range(minimum, maximum);
        Estimate = estimate;
    }

    public ExpectedValue(double minimum, double maximum)
    {
        Range = new Range(minimum, maximum);
        Estimate = minimum;
    }

    public ExpectedValue(double estimate)
    {
        Estimate = estimate;
        Range = new Range(estimate, estimate);
    }

    public Range Range { get; }
    public double Estimate { get; }
    public double Minimum => Range.Minimum;
    public double Maximum => Range.Maximum;

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
        return $"{{Estimate: {Estimate}, Range: {Range}}}";
    }
}