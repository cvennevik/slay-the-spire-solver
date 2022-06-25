using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class RecoverBaseEnergyEffectTests
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
        var nextGameState = new RecoverBaseEnergyEffect().Apply(gameState).SingleResolvedGameState();
        var expectedGameState = gameState with { Energy = baseEnergy };
        Assert.AreEqual(expectedGameState, nextGameState);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new RecoverBaseEnergyEffect(), new RecoverBaseEnergyEffect());
    }
}