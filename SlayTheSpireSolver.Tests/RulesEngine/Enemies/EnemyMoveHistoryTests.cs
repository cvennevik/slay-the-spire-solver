using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies;

[TestFixture]
public class EnemyMoveHistoryTests
{
    [Test]
    public void EmptyHistoriesAreEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory();
        var enemyMoveHistory2 = new EnemyMoveHistory();
        Assert.AreEqual(enemyMoveHistory1, enemyMoveHistory2);
    }
}