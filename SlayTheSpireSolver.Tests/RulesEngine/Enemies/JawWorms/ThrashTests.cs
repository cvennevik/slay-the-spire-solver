using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies.JawWorms;

[TestFixture]
public class ThrashTests
{
    [Test]
    public void TestEffects()
    {
        var enemyId = EnemyId.New();
        var effects = new Thrash().GetEffects(new JawWorm { Id = enemyId });
        var expectedEffects = new EffectStack(new GainEnemyArmorEffect(enemyId, 5), new AttackPlayerEffect(enemyId, 7));
        Assert.AreEqual(expectedEffects, effects);
    }
}