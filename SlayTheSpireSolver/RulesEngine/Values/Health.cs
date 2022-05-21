namespace SlayTheSpireSolver.RulesEngine.Values;

public record Health
{
    public int Amount { get; }

    public Health(int amount)
    {
        Amount = amount;
    }
}
