namespace SlayTheSpireSolver.RulesEngine.Enemies;

public abstract record Enemy
{
    public Health Health { get; init; } = new Health(1);
    public abstract IEnemyMove GetIntendedMove();

    public Enemy Damage(int amountOfDamage)
    {
        if (amountOfDamage < 0) throw new ArgumentOutOfRangeException(nameof(amountOfDamage));
        return this with { Health = new Health(Health.Amount - amountOfDamage) };
    }
}
