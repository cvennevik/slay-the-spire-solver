namespace SlayTheSpireSolver;

public record Armor
{
    public int Value { get; }

    public Armor(int value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;
    }
}
