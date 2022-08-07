namespace SlayTheSpireSolver.RulesEngine.Values;

public readonly record struct Healing
{
    public int Amount { get; }

    public Healing(int amount)
    {
        if (amount < 0) throw new ArgumentException("Healing cannot be negative");
        Amount = amount;
    }
};