using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies.JawWorms;

[TestFixture]
public class JawWormTests
{
    [Test]
    public void NewEnemiesAreEqual()
    {
        Assert.AreEqual(new JawWorm(), new JawWorm());
    }

    [Test]
    public void NewEnemiesHaveDefaultId()
    {
        Assert.AreEqual(EnemyId.Default, new JawWorm().Id);
        Assert.AreEqual(new JawWorm().Id, new JawWorm().Id);
    }

    [Test]
    public void EnemiesWithDifferentIdsAreNotEqual()
    {
        Assert.AreNotEqual(new JawWorm { Id = EnemyId.New() }, new JawWorm { Id = EnemyId.New() });
    }
}
