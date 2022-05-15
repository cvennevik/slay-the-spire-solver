namespace SlayTheSpireSolver.Enemies;

public abstract record Enemy
{
    public Health Health { get; init; } = new Health(1);
    public abstract IEnemyMove GetIntendedMove();
}
