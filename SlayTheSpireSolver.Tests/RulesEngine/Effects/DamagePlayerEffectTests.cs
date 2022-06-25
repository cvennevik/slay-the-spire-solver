using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DamagePlayerEffectTests
{
    [Test]
    [TestCase(20, 0, 10, 10, 0)]
    [TestCase(20, 0, 5, 15, 0)]
    [TestCase(20, 0, 0, 20, 0)]
    [TestCase(5, 0, 10, -5, 0)]
    [TestCase(10, 10, 5, 10, 5)]
    [TestCase(10, 10, 10, 10, 0)]
    public void Test(int initialHealth, int initialArmor, int damage, int expectedHealth, int expectedArmor)
    {
        var damagePlayerEffect = new DamagePlayerEffect(damage);
        var gameState = new GameState { PlayerHealth = initialHealth, PlayerArmor = initialArmor};
        var result = damagePlayerEffect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(new GameState { PlayerHealth = expectedHealth, PlayerArmor = expectedArmor }, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new DamagePlayerEffect(10), new DamagePlayerEffect(10));
        Assert.AreNotEqual(new DamagePlayerEffect(10), new DamagePlayerEffect(5));
    }
}