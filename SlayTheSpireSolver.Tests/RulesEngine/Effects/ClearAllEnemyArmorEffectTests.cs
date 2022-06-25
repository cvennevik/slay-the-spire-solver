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
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm(), new JawWorm()) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ClearsArmorFromSingleEnemy()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 5 }) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void ClearsArmorFromMultipleEnemies()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 5 }) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        Assert.AreEqual(expectedGameState, result);
    }
}