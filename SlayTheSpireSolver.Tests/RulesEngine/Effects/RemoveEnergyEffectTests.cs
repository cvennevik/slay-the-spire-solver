using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class RemoveEnergyEffectTests
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
        Assert.AreEqual(expectedGameState, effect.Apply(gameState).SingleResolvedGameState());
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new RemoveEnergyEffect(0), new RemoveEnergyEffect(0));
        Assert.AreEqual(new RemoveEnergyEffect(1), new RemoveEnergyEffect(1));
        Assert.AreNotEqual(new RemoveEnergyEffect(0), new RemoveEnergyEffect(1));
    }
}