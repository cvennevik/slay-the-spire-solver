namespace SlayTheSpireSolver;

public record Energy
{
    public int Amount { get; }

    public Energy(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }

    public static bool operator >(Energy a, Energy b) => a.Amount > b.Amount;
    public static bool operator >=(Energy a, Energy b) => a.Amount >= b.Amount;
    public static bool operator <(Energy a, Energy b) => a.Amount < b.Amount;
    public static bool operator <=(Energy a, Energy b) => a.Amount <= b.Amount;
    public static Energy operator +(Energy a, Energy b) => new(a.Amount + b.Amount);
    public static Energy operator -(Energy a, Energy b) => new(a < b ? 0 : a.Amount - b.Amount);
}
