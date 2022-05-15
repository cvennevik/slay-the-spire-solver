namespace SlayTheSpireSolver.Enemies;

public abstract record Enemy
{
    public Health Health { get; init; } = new Health(1);
    public abstract IEnemyMove GetIntendedMove();

    public Enemy Damage(int damageValue)
    {
        if (damageValue < 0) throw new ArgumentOutOfRangeException(nameof(damageValue));
        return this with { Health = new Health(Health.Value - damageValue) };
    }
}
