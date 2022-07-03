using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ApplyVulnerableToEnemyEffect(EnemyId Target) : TargetEnemyEffect
{
    public ApplyVulnerableToEnemyEffect() : this(EnemyId.Default) { }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}

[TestFixture]
internal class ApplyVulnerableToEnemyEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { Turn = 3 };
        var effect = new ApplyVulnerableToEnemyEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { Id = EnemyId.New() } } };
        var effect = new ApplyVulnerableToEnemyEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    [TestCase(1)]
    public void AppliesVulnerableToTargetEnemy(int vulnerableAmount)
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { Turn = 3, EnemyParty = new[] { targetEnemy } };
        var effect = new ApplyVulnerableToEnemyEffect(targetEnemy.Id);
        var result = effect.Resolve(gameState).SingleResolvedState();
        var expectedGameState = gameState with
        {
            EnemyParty = new[] { targetEnemy with { Vulnerable = vulnerableAmount } }
        };
    }
}