using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GainPlayerArmorEffect(Armor ArmorGain) : Effect
{
    public override PossibilitySet NewResolve(GameState gameState)
    {
        return gameState with { PlayerArmor = gameState.PlayerArmor + ArmorGain };
    }
}

[TestFixture]
internal class GainPlayerArmorEffectTests
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
            effect.NewResolve(gameState).Single().GameState);
    }
}