namespace SlayTheSpireSolver.RulesEngine.Values;

public record Health(int Amount)
{
    public static Health operator -(Health health, Damage damage) => new(health.Amount - damage.Amount);
    public static implicit operator Health(int amount) => new(amount);
}
