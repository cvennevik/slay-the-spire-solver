using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ClearAllEnemyArmorEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty() };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemiesHaveArmor()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy1, enemy2) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ClearsArmorFromSingleEnemy()
    {
        var enemy = new JawWorm { Id = EnemyId.New(), Armor = 5 };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(enemy with { Armor = 0 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void ClearsArmorFromMultipleEnemies()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New(), Armor = 5 };
        var enemy2 = new JawWorm { Id = EnemyId.New(), Armor = 7 };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy1, enemy2) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        var expectedGameState = new GameState
            { EnemyParty = new EnemyParty(enemy1 with { Armor = 0 }, enemy2 with { Armor = 0 }) };
        Assert.AreEqual(expectedGameState, result);
    }
}