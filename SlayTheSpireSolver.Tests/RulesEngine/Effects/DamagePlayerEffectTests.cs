using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DamagePlayerEffectTests
{
    [Test]
    public void Test()
    {
        var damagePlayerEffect = new DamagePlayerEffect(new Damage(10));
        var gameState = new GameState { PlayerHealth = 20 };
        var result = damagePlayerEffect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(new GameState { PlayerHealth = 10 }, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new DamagePlayerEffect(10), new DamagePlayerEffect(10));
    }
}