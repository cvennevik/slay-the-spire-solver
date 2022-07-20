namespace SlayTheSpireSolver.AI;

public record ExpectedValue : IComparable<ExpectedValue>
{
    public ExpectedValue(double minimum, double estimate, double maximum)
    {
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

    public ExpectedValue(double estimate, Range range)
    {
        Estimate = estimate;
        Range = range;
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