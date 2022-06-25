using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies.JawWorms;

[TestFixture]
public class ChompTests
{
    private Chomp Chomp { get; } = new();

    [Test]
    public void TestEffects()
    {
        Assert.AreEqual(new EffectStack(new AttackPlayerEffect(EnemyId.Default, 12)), Chomp.GetEffects(new JawWorm()));
    }
}
