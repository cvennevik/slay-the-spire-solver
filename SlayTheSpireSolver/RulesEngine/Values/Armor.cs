namespace SlayTheSpireSolver.RulesEngine.Values;

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

    public static bool operator >(Armor armor, Damage damage) => armor.Amount > damage.Amount;
    public static bool operator >=(Armor armor, Damage damage) => armor.Amount >= damage.Amount;
    public static bool operator <(Armor armor, Damage damage) => armor.Amount < damage.Amount;
    public static bool operator <=(Armor armor, Damage damage) => armor.Amount <= damage.Amount;

    public static Armor operator -(Armor armor, Damage damage) =>
        new(armor < damage ? 0 : armor.Amount - damage.Amount);

    
    public static implicit operator Armor(int amount) => amount > 0 ? new Armor(amount) : new Armor(0);
}
