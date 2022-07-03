using SlayTheSpireSolver.RulesEngine.Buffs;

namespace SlayTheSpireSolver.RulesEngine.Values;

public record Damage
{
    public int Amount { get; }

    public Damage(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }

    public Damage AgainstVulnerableEnemy()
    {
        return (int)(Amount * 1.5);
    }

    public static bool operator >(Damage damage, Armor armor) => damage.Amount > armor.Amount;
    public static bool operator >=(Damage damage, Armor armor) => damage.Amount >= armor.Amount;
    public static bool operator <(Damage damage, Armor armor) => damage.Amount < armor.Amount;
    public static bool operator <=(Damage damage, Armor armor) => damage.Amount <= armor.Amount;

    public static Damage operator -(Damage damage, Armor armor) =>
        new(damage < armor ? 0 : damage.Amount - armor.Amount);

    public static Damage operator +(Damage damage, Strength strength) =>
        new(damage.Amount + strength.Amount);

    public static implicit operator Damage(int amount) => amount > 0 ? new Damage(amount) : new Damage(0);

    public override string ToString()
    {
        return $"{Amount}";
    }
}