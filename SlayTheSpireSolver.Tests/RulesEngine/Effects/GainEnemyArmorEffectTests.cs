using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class GainEnemyArmorEffectTests
{
    [Test]
    public void AddsEnemyArmor()
    {
        var effect = new GainEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 5 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsToExistingEnemyArmor()
    {
        var effect = new GainEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm {Armor = 5}) };
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 10 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void OnlyAddsArmorToTargetEnemy()
    {
        var targetEnemyId = EnemyId.New();
        var otherEnemyId = EnemyId.New();
        var effect = new GainEnemyArmorEffect(targetEnemyId, new Armor(5));
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = targetEnemyId },
                new JawWorm { Id = otherEnemyId })
        };
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = targetEnemyId, Armor = new Armor(5) },
                new JawWorm { Id = otherEnemyId })
        };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var effect = new GainEnemyArmorEffect(EnemyId.Default, 1);
        var gameState = new GameState { Turn = 2 };
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var effect = new GainEnemyArmorEffect(EnemyId.Default, 5);
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Id = EnemyId.New() }) };
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new GainEnemyArmorEffect(EnemyId.Default, 10), new GainEnemyArmorEffect(EnemyId.Default, 10));
        Assert.AreNotEqual(new GainEnemyArmorEffect(EnemyId.Default, 10), new GainEnemyArmorEffect(EnemyId.Default, 5));
        Assert.AreNotEqual(new GainEnemyArmorEffect(EnemyId.Default, 10), new GainEnemyArmorEffect(EnemyId.New(), 10));
        Assert.AreNotEqual(new GainEnemyArmorEffect(EnemyId.New(), 10), new GainEnemyArmorEffect(EnemyId.New(), 10));
    }
}