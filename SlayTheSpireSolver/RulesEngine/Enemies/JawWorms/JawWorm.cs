using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record JawWorm : Enemy
{
    public IJawWormMove IntendedMove { get; init; } = new Chomp();

    private const double BellowProbability = 0.45;
    private const double ThrashProbability = 0.3;
    private const double ChompProbability = 0.25;

    public override IReadOnlyCollection<(IEnemyMove, Probability)> GetNextPossibleMoves()
    {
        if (PreviousMoves.Count == 0)
        {
            return new (IEnemyMove, Probability)[] { (new Chomp(), new Probability(1)) };
        }

        if (PreviousMoves[^1] is Bellow)
        {
            const double remainingOdds = 1 - BellowProbability;
            return new (IEnemyMove, Probability)[]
            {
                (new Thrash(), ThrashProbability / remainingOdds),
                (new Chomp(), new Probability(ChompProbability / remainingOdds))
            };
        }

        return new (IEnemyMove, Probability)[]
        {
            (new Bellow(), BellowProbability),
            (new Thrash(), ThrashProbability),
            (new Chomp(), ChompProbability)
        };
    }

    protected override IEnemyMove GetIntendedMove() => IntendedMove;
}
