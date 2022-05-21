namespace SlayTheSpireSolver.RulesEngine.Values;

public record Health
{
    public int Amount { get; }

    public Health(int amount)
    {
        Amount = amount;
    }

    public static Health operator -(Health health, Damage damage) => new(health.Amount - damage.Amount);
}
