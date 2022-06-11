namespace SlayTheSpireSolver.RulesEngine.Values;

public record Health(int Amount)
{
    public static Health operator -(Health health, Damage damage) => new(health.Amount - damage.Amount);
}
