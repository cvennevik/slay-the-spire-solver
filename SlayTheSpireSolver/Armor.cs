namespace SlayTheSpireSolver;

public record Armor
{
    public int Amount { get; }

    public Armor(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }
}
