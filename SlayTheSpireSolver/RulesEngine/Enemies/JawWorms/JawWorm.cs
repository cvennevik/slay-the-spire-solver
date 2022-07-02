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
        IEnemyMove chomp = new Chomp();
        if (PreviousMoves.Count == 0)
        {
            return new[] { (chomp, new Probability(1)) };
        }

        IEnemyMove thrash = new Thrash();
        if (PreviousMoves[^1] is Bellow)
        {
            const double remainingProbability = 1 - BellowProbability;
            return new (IEnemyMove, Probability)[]
            {
                (thrash, ThrashProbability / remainingProbability),
                (chomp, ChompProbability / remainingProbability)
            };
        }

        IEnemyMove bellow = new Bellow();
        if (PreviousMoves[^1] is Chomp)
        {
            const double remainingProbability = 1 - ChompProbability;
            return new (IEnemyMove, Probability)[]
            {
                (bellow, BellowProbability / remainingProbability),
                (thrash, ThrashProbability / remainingProbability)
            };
        }

        if (PreviousMoves.Count >= 2 && PreviousMoves[^1] is Thrash && PreviousMoves[^2] is Thrash)
        {
            const double remainingProbability = 1 - ThrashProbability;
            return new (IEnemyMove, Probability)[]
            {
                (bellow, BellowProbability / remainingProbability),
                (chomp, ChompProbability / remainingProbability)
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
