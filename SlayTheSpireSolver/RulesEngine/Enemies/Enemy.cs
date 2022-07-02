using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Debuffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public abstract record Enemy
{
    public EnemyId Id { get; init; } = EnemyId.Default;
    public Health Health { get; init; } = 1;
    public Armor Armor { get; init; } = 0;
    public Strength Strength { get; init; } = 0;
    public Vulnerable Vulnerable { get; init; } = new(0);
    public EnemyMoveHistory PreviousMoves = new();
    public abstract IEnemyMove IntendedMove { get; init; }
    public abstract IReadOnlyCollection<(IEnemyMove, Probability)> GetNextPossibleMoves();

    public EffectStack GetMoveEffects()
    {
        return IntendedMove.GetEffects(this);
    }
}
