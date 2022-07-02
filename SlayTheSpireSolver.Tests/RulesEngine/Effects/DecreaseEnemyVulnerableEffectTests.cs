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
        var gameState = new GameState { EnemyParty = new[] { new JawWorm {Id = EnemyId.New()} } };
        var effect = new DecreaseEnemyVulnerableEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }
}