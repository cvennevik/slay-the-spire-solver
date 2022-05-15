namespace SlayTheSpireSolver;

public record Player
{
    public Health Health { get; init; } = new Health(1);
    public Armor Armor { get; init; } = new Armor(0);
}
