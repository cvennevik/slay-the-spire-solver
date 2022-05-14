namespace SlayTheSpireSolver.Enemies;

public abstract record Enemy
{
    public Health Health { get; init; }
    public abstract IEnemyMove GetIntendedMove();
}
