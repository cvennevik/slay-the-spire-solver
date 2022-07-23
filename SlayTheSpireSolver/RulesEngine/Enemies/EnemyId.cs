using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Enemies;

public readonly record struct EnemyId
{
    public static readonly EnemyId Default = new();

    // Arbitrary string ID makes enemies' ToString() more readable
    private readonly string _printedId;

    public EnemyId()
    {
        _printedId = GeneratePrintedId();
    }

    private static string GeneratePrintedId()
    {
        var random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public override string ToString()
    {
        return $"{_printedId}";
    }
}

[TestFixture]
internal class EnemyIdTests
{
    [Test]
    public void TestEquality()
    {
        var enemyId1 = new EnemyId();
        var enemyId2 = new EnemyId();
        Assert.AreEqual(enemyId1, enemyId1);
        Assert.AreEqual(enemyId2, enemyId2);
        Assert.AreNotEqual(enemyId1, enemyId2);
    }

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
        Assert.AreNotEqual(new JawWorm { Id = new EnemyId() }, new JawWorm { Id = new EnemyId() });
    }
}