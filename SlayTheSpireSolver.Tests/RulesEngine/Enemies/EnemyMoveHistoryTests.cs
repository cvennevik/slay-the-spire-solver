using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

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

    [Test]
    public void SingleMoveHistoriesAreEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp());
        Assert.AreEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void SingleMoveHistoriesAreDifferent()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Bellow());
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void DifferentLengthHistoriesAreDifferent1()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory();
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void DifferentLengthHistoriesAreDifferent2()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp(), new Chomp());
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void MultipleMoveHistoriesAreEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Chomp(), new Bellow());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp(), new Bellow());
        Assert.AreEqual(enemyMoveHistory1, enemyMoveHistory2);
    }

    [Test]
    public void MultipleMoveHistoriesWithDifferentOrderAreNotEqual()
    {
        var enemyMoveHistory1 = new EnemyMoveHistory(new Bellow(), new Chomp());
        var enemyMoveHistory2 = new EnemyMoveHistory(new Chomp(), new Bellow());
        Assert.AreNotEqual(enemyMoveHistory1, enemyMoveHistory2);
    }
}