using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class GainPlayerArmorEffectTests
{
    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(5, 0, 5)]
    [TestCase(5, 5, 10)]
    public void TestEffect(int initialPlayerArmor, int effectAmount, int expectedPlayerArmor)
    {
        var gameState = new GameState { PlayerArmor = initialPlayerArmor };
        var effect = new GainPlayerArmorEffect(effectAmount);
        Assert.AreEqual(new GameState { PlayerArmor = expectedPlayerArmor },
            effect.Resolve(gameState).SingleResolvedState());
    }
}