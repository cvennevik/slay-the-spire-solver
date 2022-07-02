using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DecreaseEnemyVulnerableEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { Id = EnemyId.New(), Vulnerable = 2 } } };
        var effect = new DecreaseEnemyVulnerableEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ReducesVulnerableOfTargetEnemy()
    {
        var gameState = new GameState { Turn = 2, EnemyParty = new[] { new JawWorm { Vulnerable = 2 } } };
        var effect = new DecreaseEnemyVulnerableEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState with { EnemyParty = new[] { new JawWorm { Vulnerable = 1 } } }, result);
    }

    [Test]
    public void DoesNothingIfVulnerableAlreadyZero()
    {
        var gameState = new GameState { Turn = 2, EnemyParty = new[] { new JawWorm { Vulnerable = 0 } } };
        var effect = new DecreaseEnemyVulnerableEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void OnlyReducesVulnerableOfTargetEnemy()
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), Vulnerable = 3 };
        var otherEnemy = new JawWorm { Id = EnemyId.New(), Vulnerable = 2 };
        var gameState = new GameState { Turn = 2, EnemyParty = new[] { targetEnemy, otherEnemy } };
        var effect = new DecreaseEnemyVulnerableEffect(targetEnemy.Id);
        var result = effect.Resolve(gameState).SingleResolvedState();
        var expectedResult = gameState with { EnemyParty = new[] { targetEnemy with { Vulnerable = 2 }, otherEnemy } };
        Assert.AreEqual(expectedResult, result);
    }
}