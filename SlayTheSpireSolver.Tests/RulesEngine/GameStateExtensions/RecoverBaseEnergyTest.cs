﻿using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class RecoverBaseEnergyTest
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
        var nextGameState = gameState.RecoverBaseEnergy();
        var expectedGameState = gameState with { Energy = baseEnergy };
        Assert.AreEqual(expectedGameState, nextGameState);
    }
}
