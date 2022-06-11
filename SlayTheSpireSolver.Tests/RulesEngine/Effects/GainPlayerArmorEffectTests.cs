using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class GainPlayerArmorEffectTests
{
    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(5, 0, 5)]
    [TestCase(5, 5, 10)]
    [TestCase(5, -10, 5)]
    [TestCase(0, -10, 0)]
    public void TestEffect(int initialPlayerArmor, int effectAmount, int expectedPlayerArmor)
    {
        var gameState = new GameState { PlayerArmor = new Armor(initialPlayerArmor) };
        var effect = new GainPlayerArmorEffect(effectAmount);
        Assert.AreEqual(new GameState { PlayerArmor = new Armor(expectedPlayerArmor) },
            effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(0, -10)]
    [TestCase(5, 5)]
    public void TestEqual(int amountA, int amountB)
    {
        Assert.AreEqual(new GainPlayerArmorEffect(amountA), new GainPlayerArmorEffect(amountB));
    }

    [Test]
    [TestCase(0, 5)]
    [TestCase(1, -10)]
    public void TestNotEqual(int amountA, int amountB)
    {
        Assert.AreNotEqual(new GainPlayerArmorEffect(amountA), new GainPlayerArmorEffect(amountB));
    }
}