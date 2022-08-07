using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Debuffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public abstract record Enemy
{
    public EnemyId Id { get; init; } = EnemyId.Default;
    public Health Health { get; init; } = new(1, 1);
    public Armor Armor { get; init; } = 0;
    public Strength Strength { get; init; } = 0;
    public Vulnerable Vulnerable { get; init; } = 0;
    public EnemyMoveHistory PreviousMoves = new();
    public abstract EnemyMove IntendedMove { get; init; }
    public abstract IReadOnlyCollection<(EnemyMove, Probability)> GetNextPossibleMoves();

    public EffectStack GetMoveEffects()
    {
        return IntendedMove.GetEffects(this);
    }
}