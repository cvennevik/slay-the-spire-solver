using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RemoveEnergyEffect(Energy EnergyToRemove) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.Energy - EnergyToRemove };
    }
}

[TestFixture]
internal class RemoveEnergyEffectTests
{
    [Test]
    [TestCase(0, 0, 0)]
    [TestCase(1, 0, 1)]
    [TestCase(0, 1, 0)]
    [TestCase(1, 1, 0)]
    [TestCase(2, 1, 1)]
    public void TestEffect(int initialEnergy, int effectAmount, int expectedEnergy)
    {
        var gameState = new GameState { Energy = initialEnergy };
        var effect = new RemoveEnergyEffect(effectAmount);
        var expectedGameState = new GameState { Energy = expectedEnergy };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }
}