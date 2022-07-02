using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies;

[TestFixture]
public class JawWormTests
{
    [Test]
    public void AlwaysPicksChompWhenNoPreviousMoves()
    {
        var jawWorm = new JawWorm();
        var nextPossibleMoves = jawWorm.GetNextPossibleMoves();
        Assert.AreEqual((new Chomp(), new Probability(1)), nextPossibleMoves.Single());
    }

    [Test]
    public void PicksAnyMoveAfterChomp()
    {
        var jawWorm = new JawWorm { PreviousMoves = new[] { new Chomp() } };
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
    }
}