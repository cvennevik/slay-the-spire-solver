using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record JawWorm : Enemy
{
    public IJawWormMove IntendedMove { get; init; } = new Chomp();

    public override IReadOnlyCollection<(IEnemyMove, Probability)> GetNextPossibleMoves()
    {
        throw new NotImplementedException();
    }

    protected override IEnemyMove GetIntendedMove() => IntendedMove;
}
