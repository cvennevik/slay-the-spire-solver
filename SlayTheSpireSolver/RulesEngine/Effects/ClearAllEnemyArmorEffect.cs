using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearAllEnemyArmorEffect : Effect
{
    public override ResolvablePossibilitySet OldResolve(GameState gameState)
    {
        var newEnemyParty = gameState.EnemyParty;
        foreach (var enemy in gameState.EnemyParty)
        {
            newEnemyParty = newEnemyParty.ModifyEnemy(enemy.Id, x => x with { Armor = 0 });
        }

        return gameState with { EnemyParty = newEnemyParty };
    }
}

[TestFixture]
internal class ClearAllEnemyArmorEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty() };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.OldResolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemiesHaveArmor()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy1, enemy2) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.OldResolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ClearsArmorFromSingleEnemy()
    {
        var enemy = new JawWorm { Id = EnemyId.New(), Armor = 5 };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.OldResolve(gameState).SingleResolvedState();
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
        var result = effect.OldResolve(gameState).SingleResolvedState();
        var expectedEnemyParty = new EnemyParty(enemy1 with { Armor = 0 }, enemy2 with { Armor = 0 });
        var expectedGameState = gameState with { EnemyParty = expectedEnemyParty };
        Assert.AreEqual(expectedGameState, result);
    }
}