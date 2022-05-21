namespace SlayTheSpireSolver.RulesEngine.Values;

public record Damage
{
    public int Amount { get; }

    public Damage(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }
}