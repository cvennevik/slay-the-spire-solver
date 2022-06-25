namespace SlayTheSpireSolver.RulesEngine.Buffs;

public record Strength
{
    public int Amount { get; }

    public Strength(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }

    public static bool operator >(Strength a, Strength b) => a.Amount > b.Amount;
    public static bool operator >=(Strength a, Strength b) => a.Amount >= b.Amount;
    public static bool operator <(Strength a, Strength b) => a.Amount < b.Amount;
    public static bool operator <=(Strength a, Strength b) => a.Amount <= b.Amount;
    public static Strength operator +(Strength a, Strength b) => new(a.Amount + b.Amount);
    public static Strength operator -(Strength a, Strength b) => new(a < b ? 0 : a.Amount - b.Amount);

    public static implicit operator Strength(int amount) => amount > 0 ? new Strength(amount) : new Strength(0);

    public override string ToString()
    {
        return $"{Amount}";
    }
}