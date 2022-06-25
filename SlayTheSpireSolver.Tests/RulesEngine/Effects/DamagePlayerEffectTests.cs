using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DamagePlayerEffectTests
{
    [Test]
    [TestCase(10, 20, 10)]
    [TestCase(5, 20, 15)]
    [TestCase(0, 20, 20)]
    [TestCase(10, 5, -5)]
    public void Test(int damage, int initialPlayerHealth, int expectedPlayerHealth)
    {
        var damagePlayerEffect = new DamagePlayerEffect(damage);
        var gameState = new GameState { PlayerHealth = initialPlayerHealth };
        var result = damagePlayerEffect.Apply(gameState).SingleStableGameState();
        Assert.AreEqual(new GameState { PlayerHealth = expectedPlayerHealth }, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new DamagePlayerEffect(10), new DamagePlayerEffect(10));
        Assert.AreNotEqual(new DamagePlayerEffect(10), new DamagePlayerEffect(5));
    }
}