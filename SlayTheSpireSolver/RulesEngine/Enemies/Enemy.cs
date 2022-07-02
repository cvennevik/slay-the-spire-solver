using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public abstract record Enemy
{
    public EnemyId Id { get; init; } = EnemyId.Default;
    public Health Health { get; init; } = 1;
    public Armor Armor { get; init; } = 0;
    public Strength Strength { get; init; } = 0;
    public EnemyMoveHistory PreviousMoves = new();
    protected abstract IEnemyMove GetIntendedMove();

    public EffectStack GetMoveEffects()
    {
        return GetIntendedMove().GetEffects(this);
    }
}
