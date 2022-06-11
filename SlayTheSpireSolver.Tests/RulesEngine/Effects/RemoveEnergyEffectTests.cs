using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class RemoveEnergyEffectTests
{
    [Test]
    [TestCase(0, -1, 0)]
    [TestCase(1, -1, 1)]
    [TestCase(0, 0, 0)]
    [TestCase(1, 0, 1)]
    [TestCase(0, 1, 0)]
    [TestCase(1, 1, 0)]
    [TestCase(2, 1, 1)]
    public void TestEffect(int initialEnergyAmount, int effectAmount, int expectedEnergyAmount)
    {
        var gameState = new GameState { Energy = new Energy(initialEnergyAmount) };
        var effect = new RemoveEnergyEffect(effectAmount);
        var expectedGameState = new GameState { Energy = new Energy(expectedEnergyAmount) };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new RemoveEnergyEffect(0), new RemoveEnergyEffect(0));
        Assert.AreEqual(new RemoveEnergyEffect(0), new RemoveEnergyEffect(-1));
        Assert.AreEqual(new RemoveEnergyEffect(1), new RemoveEnergyEffect(1));
        Assert.AreNotEqual(new RemoveEnergyEffect(0), new RemoveEnergyEffect(1));
    }
}