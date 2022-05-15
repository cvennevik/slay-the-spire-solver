namespace SlayTheSpireSolver;

public record Energy
{
    public int Amount { get; }

    public Energy(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }
}
