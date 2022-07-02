using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Debuffs;
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
}