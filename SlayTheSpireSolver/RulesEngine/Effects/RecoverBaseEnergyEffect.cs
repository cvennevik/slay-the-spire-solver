using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RecoverBaseEnergyEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Energy = gameState.BaseEnergy };
    }
}

[TestFixture]
internal class RecoverBaseEnergyEffectTests
{
    [Test]
    [TestCase(3, 0)]
    [TestCase(3, 1)]
    [TestCase(3, 2)]
    [TestCase(3, 3)]
    [TestCase(3, 4)]
    [TestCase(3, 10)]
    [TestCase(4, 3)]
    [TestCase(4, 4)]
    public void ResetsEnergyToBaseEnergy(int baseEnergy, int initialEnergy)
    {
        var gameState = new GameState
        {
            BaseEnergy = baseEnergy,
            Energy = initialEnergy
        };
        var nextGameState = new RecoverBaseEnergyEffect().Resolve(gameState).Single().GameState;
        var expectedGameState = gameState with { Energy = baseEnergy };
        Assert.AreEqual(expectedGameState, nextGameState);
    }
}