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
    public void TestEffect(int initialPlayerArmor, int effectAmount, int expectedPlayerArmor)
    {
        var gameState = new GameState { PlayerArmor = new Armor(initialPlayerArmor) };
        var effect = new GainPlayerArmorEffect(new Armor(effectAmount));
        Assert.AreEqual(new GameState { PlayerArmor = new Armor(expectedPlayerArmor) },
            effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(5, 5)]
    public void TestEqual(int amountA, int amountB)
    {
        Assert.AreEqual(new GainPlayerArmorEffect(new Armor(amountA)), new GainPlayerArmorEffect(new Armor(amountB)));
    }

    [Test]
    [TestCase(0, 5)]
    [TestCase(1, 0)]
    public void TestNotEqual(int amountA, int amountB)
    {
        Assert.AreNotEqual(new GainPlayerArmorEffect(new Armor(amountA)), new GainPlayerArmorEffect(new Armor(amountB)));
    }
}