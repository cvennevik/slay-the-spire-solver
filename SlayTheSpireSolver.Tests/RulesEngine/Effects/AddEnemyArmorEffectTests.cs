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
        var effect = new AddEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var result = effect.Resolve(gameState).SingleResolvedState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 5 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsToExistingEnemyArmor()
    {
        var effect = new AddEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm {Armor = 5}) };
        var result = effect.Resolve(gameState).SingleResolvedState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 10 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void OnlyAddsArmorToTargetEnemy()
    {
        var targetEnemyId = EnemyId.New();
        var otherEnemyId = EnemyId.New();
        var effect = new AddEnemyArmorEffect(targetEnemyId, new Armor(5));
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = targetEnemyId },
                new JawWorm { Id = otherEnemyId })
        };
        var result = effect.Resolve(gameState).SingleResolvedState();
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
        var effect = new AddEnemyArmorEffect(EnemyId.Default, 1);
        var gameState = new GameState { Turn = 2 };
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var effect = new AddEnemyArmorEffect(EnemyId.Default, 5);
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Id = EnemyId.New() }) };
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }
}