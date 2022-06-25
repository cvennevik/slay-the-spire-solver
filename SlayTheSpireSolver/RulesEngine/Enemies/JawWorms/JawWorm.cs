using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record JawWorm : Enemy
{
    public IJawWormMove IntendedMove { get; init; } = new Chomp();

    public override IEnemyMove GetIntendedMove() => IntendedMove;

    public override EffectStack GetMoveEffects()
    {
        return IntendedMove.GetEffects(this);
    }
}
