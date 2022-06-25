namespace SlayTheSpireSolver.RulesEngine.Utils;

public abstract class NonNegativeInteger<T> where T : NonNegativeInteger<T>
{
    public int Amount { get; }

    public NonNegativeInteger(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }
}