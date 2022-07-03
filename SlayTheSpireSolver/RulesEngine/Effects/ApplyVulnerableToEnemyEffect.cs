using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Debuffs;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ApplyVulnerableToEnemyEffect(EnemyId Target, Vulnerable VulnerableToApply) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;
        return gameState.ModifyEnemy(Target, enemy => enemy with { Vulnerable = VulnerableToApply });
    }
}

[TestFixture]
internal class ApplyVulnerableToEnemyEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { Turn = 3 };
        var effect = new ApplyVulnerableToEnemyEffect(EnemyId.Default, new Vulnerable(1));
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { Id = EnemyId.New() } } };
        var effect = new ApplyVulnerableToEnemyEffect(EnemyId.Default, new Vulnerable(1));
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    [TestCase(1)]
    public void AppliesVulnerableToTargetEnemy(int vulnerableAmount)
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { Turn = 3, EnemyParty = new[] { targetEnemy } };
        var effect = new ApplyVulnerableToEnemyEffect(targetEnemy.Id, vulnerableAmount);
        var result = effect.Resolve(gameState).SingleResolvedState();
        var expectedGameState = gameState with
        {
            EnemyParty = new[] { targetEnemy with { Vulnerable = vulnerableAmount } }
        };
        Assert.AreEqual(expectedGameState, result);
    }
}