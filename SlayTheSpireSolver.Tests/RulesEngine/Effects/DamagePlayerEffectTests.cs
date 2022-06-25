using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DamagePlayerEffectTests
{
    [Test]
    [TestCase(20, 10, 10)]
    [TestCase(20, 5, 15)]
    [TestCase(20, 0, 20)]
    [TestCase(5, 10, -5)]
    public void Test(int initialPlayerHealth, int damage, int expectedPlayerHealth)
    {
        var damagePlayerEffect = new DamagePlayerEffect(damage);
        var gameState = new GameState { PlayerHealth = initialPlayerHealth };
        var result = damagePlayerEffect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(new GameState { PlayerHealth = expectedPlayerHealth }, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new DamagePlayerEffect(10), new DamagePlayerEffect(10));
        Assert.AreNotEqual(new DamagePlayerEffect(10), new DamagePlayerEffect(5));
    }
}