using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

public record JawWorm : Enemy
{
    private const double BellowProbability = 0.45;
    private const double ThrashProbability = 0.3;
    private const double ChompProbability = 0.25;
    public override EnemyMove IntendedMove { get; init; } = new Chomp();

    public override IReadOnlyCollection<(EnemyMove, Probability)> GetNextPossibleMoves()
    {
        if (PreviousMoves.Count == 0) return new (EnemyMove, Probability)[] { (new Chomp(), new Probability(1)) };

        if (PreviousMoves[^1] is Bellow)
        {
            const double remainingProbability = 1 - BellowProbability;
            return new (EnemyMove, Probability)[]
            {
                (new Thrash(), ThrashProbability / remainingProbability),
                (new Chomp(), ChompProbability / remainingProbability)
            };
        }

        if (PreviousMoves[^1] is Chomp)
        {
            const double remainingProbability = 1 - ChompProbability;
            return new (EnemyMove, Probability)[]
            {
                (new Bellow(), BellowProbability / remainingProbability),
                (new Thrash(), ThrashProbability / remainingProbability)
            };
        }

        if (PreviousMoves.Count >= 2 && PreviousMoves[^1] is Thrash && PreviousMoves[^2] is Thrash)
        {
            const double remainingProbability = 1 - ThrashProbability;
            return new (EnemyMove, Probability)[]
            {
                (new Bellow(), BellowProbability / remainingProbability),
                (new Chomp(), ChompProbability / remainingProbability)
            };
        }

        return new (EnemyMove, Probability)[]
        {
            (new Bellow(), BellowProbability),
            (new Thrash(), ThrashProbability),
            (new Chomp(), ChompProbability)
        };
    }
}

[TestFixture]
internal class JawWormTests
{
    [Test]
    public void AlwaysPicksChompWhenNoPreviousMoves()
    {
        var jawWorm = new JawWorm();
        var nextPossibleMoves = jawWorm.GetNextPossibleMoves();
        Assert.AreEqual((new Chomp(), new Probability(1)), nextPossibleMoves.Single());
    }

    [Test]
    public void PicksAnyMoveAfterSingleThrash()
    {
        var jawWorm = new JawWorm { PreviousMoves = new[] { new Thrash() } };
        var nextPossibleMoves = jawWorm.GetNextPossibleMoves();
        Assert.AreEqual(3, nextPossibleMoves.Count);
        Assert.Contains((new Chomp(), new Probability(0.25)), nextPossibleMoves.ToList());
        Assert.Contains((new Thrash(), new Probability(0.3)), nextPossibleMoves.ToList());
        Assert.Contains((new Bellow(), new Probability(0.45)), nextPossibleMoves.ToList());
    }

    [Test]
    public void DoesNotPickBellowTwiceInARow()
    {
        var jawWorm = new JawWorm { PreviousMoves = new[] { new Bellow() } };
        var nextPossibleMoves = jawWorm.GetNextPossibleMoves();
        Assert.AreEqual(2, nextPossibleMoves.Count);
        Assert.Contains((new Chomp(), new Probability(0.25 / 0.55)), nextPossibleMoves.ToList());
        Assert.Contains((new Thrash(), new Probability(0.3 / 0.55)), nextPossibleMoves.ToList());
    }

    [Test]
    public void DoesNotPickChompTwiceInARow()
    {
        var jawWorm = new JawWorm { PreviousMoves = new[] { new Chomp() } };
        var nextPossibleMoves = jawWorm.GetNextPossibleMoves();
        Assert.AreEqual(2, nextPossibleMoves.Count);
        Assert.Contains((new Thrash(), new Probability(0.3 / 0.75)), nextPossibleMoves.ToList());
        Assert.Contains((new Bellow(), new Probability(0.45 / 0.75)), nextPossibleMoves.ToList());
    }

    [Test]
    public void DoesNotPickThrashThreeTimesInARow()
    {
        var jawWorm = new JawWorm { PreviousMoves = new[] { new Thrash(), new Thrash() } };
        var nextPossibleMoves = jawWorm.GetNextPossibleMoves();
        Assert.AreEqual(2, nextPossibleMoves.Count);
        Assert.Contains((new Chomp(), new Probability(0.25 / 0.7)), nextPossibleMoves.ToList());
        Assert.Contains((new Bellow(), new Probability(0.45 / 0.7)), nextPossibleMoves.ToList());
    }
}
