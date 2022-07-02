using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record JawWorm : Enemy
{
    public IJawWormMove IntendedMove { get; init; } = new Chomp();

    public override IReadOnlyCollection<(IEnemyMove, Probability)> GetNextPossibleMoves()
    {
        if (PreviousMoves.Count == 0)
        {
            return new (IEnemyMove, Probability)[] { (new Chomp(), new Probability(1)) };
        }

        var test = PreviousMoves[^1];

        return new (IEnemyMove, Probability)[]
        {
            (new Bellow(), new Probability(0.45)),
            (new Thrash(), new Probability(0.3)),
            (new Chomp(), new Probability(0.25))
        };
    }

    protected override IEnemyMove GetIntendedMove() => IntendedMove;
}
