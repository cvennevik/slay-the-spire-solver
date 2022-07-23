using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyArmorEffect(EnemyId EnemyId, Armor ArmorGain) : Effect
{
    public virtual PossibilitySet Resolve(GameState gameState)
    {
        return gameState.ModifyEnemy(EnemyId,
            enemy => enemy with { Armor = enemy.Armor + ArmorGain });
    }
}

[TestFixture]
internal class AddEnemyArmorEffectTests
{
    [Test]
    public void AddsEnemyArmor()
    {
        var effect = new AddEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var result = effect.Resolve(gameState).Single().GameState;
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 5 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void AddsToExistingEnemyArmor()
    {
        var effect = new AddEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 5 }) };
        var result = effect.Resolve(gameState).Single().GameState;
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
        var result = effect.Resolve(gameState).Single().GameState;
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
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var effect = new AddEnemyArmorEffect(EnemyId.Default, 5);
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Id = EnemyId.New() }) };
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }
}