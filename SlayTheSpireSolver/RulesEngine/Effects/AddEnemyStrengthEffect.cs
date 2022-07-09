using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddEnemyStrengthEffect(EnemyId EnemyId, Strength StrengthGain) : Effect
{
    public override ResolvablePossibilitySet OldResolve(GameState gameState)
    {
        return gameState.ModifyEnemy(EnemyId,
            enemy => enemy with { Strength = enemy.Strength + StrengthGain });
    }
}

[TestFixture]
internal class AddEnemyStrengthEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty() };
        var effect = new AddEnemyStrengthEffect(EnemyId.Default, 5);
        var result = effect.NewResolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithMatchingId()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = EnemyId.New() }, new JawWorm { Id = EnemyId.New() })
        };
        var effect = new AddEnemyStrengthEffect(EnemyId.Default, 5);
        var result = effect.NewResolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void AddsStrengthToTargetEnemy()
    {
        var targetId = EnemyId.New();
        var otherEnemyId = EnemyId.New();
        var gameState = new GameState
        {
            EnemyParty = new[] { new JawWorm { Id = targetId }, new JawWorm { Id = otherEnemyId } }
        };
        var effect = new AddEnemyStrengthEffect(targetId, 5);
        var result = effect.NewResolve(gameState).Single().GameState;
        var expectedResult = new GameState
        {
            EnemyParty = new[] { new JawWorm { Id = targetId, Strength = 5 }, new JawWorm { Id = otherEnemyId } }
        };
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void AddsStrengthToEnemyWithExistingStrength()
    {
        var gameState = new GameState
        {
            EnemyParty = new[] { new JawWorm { Strength = 4 } }
        };
        var effect = new AddEnemyStrengthEffect(EnemyId.Default, 5);
        var result = effect.NewResolve(gameState).Single().GameState;
        var expectedResult = new GameState
        {
            EnemyParty = new[] { new JawWorm { Strength = 9 } }
        };
        Assert.AreEqual(expectedResult, result);
    }
}