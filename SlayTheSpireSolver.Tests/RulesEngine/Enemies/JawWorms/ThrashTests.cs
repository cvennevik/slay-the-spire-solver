using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Enemies.JawWorms;

[TestFixture]
public class ThrashTests
{
    [Test]
    [TestCase(70, 63, 0, 5)]
    [TestCase(7, 0, 2, 7)]
    [TestCase(5, -2, 5, 10)]
    public void TestPlayerWithoutArmor(int initialPlayerHealth, int expectedPlayerHealth, int initialEnemyArmor,
        int expectedEnemyArmor)
    {
        var gameState = new GameState
        {
            PlayerHealth = initialPlayerHealth,
            PlayerArmor = 0,
            EnemyParty = new EnemyParty(new JawWorm {Armor = initialEnemyArmor})
        };
        var resolvedGameState = new Thrash().Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = expectedPlayerHealth,
            PlayerArmor = 0,
            EnemyParty = new EnemyParty(new JawWorm {Armor = expectedEnemyArmor})
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void TestPlayerWithMoreArmorThanDamage()
    {
        var gameState = new GameState
        {
            PlayerHealth = 10,
            PlayerArmor = 10,
            EnemyParty = new EnemyParty(new JawWorm {Armor = 0})
        };
        var resolvedGameState = new Thrash().Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = 10,
            PlayerArmor = 3,
            EnemyParty = new EnemyParty(new JawWorm {Armor = 5})
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }

    [Test]
    public void TestPlayerWithLessArmorThanDamage()
    {
        var gameState = new GameState
        {
            PlayerHealth = 10,
            PlayerArmor = 5,
            EnemyParty = new EnemyParty(new JawWorm {Armor = 0})
        };
        var resolvedGameState = new Thrash().Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = 8,
            PlayerArmor = 0,
            EnemyParty = new EnemyParty(new JawWorm {Armor = 5})
        };
        Assert.AreEqual(expectedGameState, resolvedGameState);
    }
}