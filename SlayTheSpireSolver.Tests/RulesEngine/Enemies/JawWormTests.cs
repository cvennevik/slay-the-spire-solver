using System.Collections.Generic;
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
        Assert.AreEqual((new Chomp(), new Probability(1)), jawWorm.GetNextPossibleMoves().Single());
    }

    [Test]
    public void PicksAnyMoveAfterChomp()
    {
        var jawWorm = new JawWorm { PreviousMoves = new[] { new Chomp() } };
        var nextPossibleMoves = jawWorm.GetNextPossibleMoves();
    }
}