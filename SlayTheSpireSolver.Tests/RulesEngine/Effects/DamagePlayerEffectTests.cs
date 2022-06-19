using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DamagePlayerEffectTests
{
    [Test]
    public void Test()
    {
        var damagePlayerEffect = new DamagePlayerEffect();
        var gameState = new GameState { PlayerHealth = 20 };
    }
}