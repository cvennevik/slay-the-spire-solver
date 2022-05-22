using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public abstract record Enemy
{
    public Health Health { get; init; } = new(1);
    public abstract IEnemyMove GetIntendedMove();

    public Enemy DealDamage(Damage damage)
    {
        return this with { Health = Health - damage };
    }
}
