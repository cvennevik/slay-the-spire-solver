namespace SlayTheSpireSolver;

public record Health
{
    public int Amount { get; }

    public Health(int amount)
    {
        Amount = amount;
    }
}
