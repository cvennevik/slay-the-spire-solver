using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AddEnemyStrengthEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty() };
        var effect = new AddEnemyStrengthEffect(EnemyId.Default, 5);
        var result = effect.Resolve(gameState).SingleResolvedGameState();
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
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void AddsStrengthToTargetEnemy()
    {
        var targetId = EnemyId.New();
        var otherEnemyId = EnemyId.New();
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = targetId },
                new JawWorm { Id = otherEnemyId })
        };
        var effect = new AddEnemyStrengthEffect(targetId, 5);
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedResult = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = targetId, Strength = 5 },
                new JawWorm { Id = otherEnemyId })
        };
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)),
            new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)));
        Assert.AreNotEqual(new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)),
            new AddEnemyStrengthEffect(EnemyId.Default, new Strength(2)));
        Assert.AreNotEqual(new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)),
            new AddEnemyStrengthEffect(EnemyId.New(), new Strength(1)));
    }
}