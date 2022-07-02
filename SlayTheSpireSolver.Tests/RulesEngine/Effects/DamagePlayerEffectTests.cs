using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.TestHelpers;

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
    [TestCase(10, 10, 11, 9, 0)]
    [TestCase(10, 10, 25, -5, 0)]
    [TestCase(10, 10, 0, 10, 10)]
    public void Test(int initialHealth, int initialArmor, int damage, int expectedHealth, int expectedArmor)
    {
        var damagePlayerEffect = new DamagePlayerEffect(damage);
        var gameState = new GameState { PlayerHealth = initialHealth, PlayerArmor = initialArmor};
        var result = damagePlayerEffect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(new GameState { PlayerHealth = expectedHealth, PlayerArmor = expectedArmor }, result);
    }
}