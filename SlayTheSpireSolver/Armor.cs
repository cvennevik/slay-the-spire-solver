namespace SlayTheSpireSolver;

public record Armor
{
    public int Amount { get; }

    public Armor(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }

    public static Armor operator +(Armor a, Armor b) => new(a.Amount + b.Amount);
    public static Armor operator -(Armor a, Armor b) => new(a.Amount < b.Amount ? 0 : a.Amount - b.Amount);
}
