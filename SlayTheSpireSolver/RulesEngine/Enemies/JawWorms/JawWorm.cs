using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record JawWorm : Enemy
{
    public IJawWormMove IntendedMove { get; init; } = new Chomp();

    private const double BaseChompProbability = 0.25;
    private const double BaseThrashProbability = 0.3;

    public override IReadOnlyCollection<(IEnemyMove, Probability)> GetNextPossibleMoves()
    {
        if (PreviousMoves.Count == 0)
        {
            return new (IEnemyMove, Probability)[] { (new Chomp(), new Probability(1)) };
        }

        if (PreviousMoves[^1] is Bellow)
        {
            return new (IEnemyMove, Probability)[]
            {
                (new Thrash(), new Probability(0.3 / 0.55)),
                (new Chomp(), new Probability(0.25 / 0.55))
            };
        }

        return new (IEnemyMove, Probability)[]
        {
            (new Bellow(), new Probability(0.45)),
            (new Thrash(), new Probability(0.3)),
            (new Chomp(), new Probability(0.25))
        };
    }

    protected override IEnemyMove GetIntendedMove() => IntendedMove;
}
