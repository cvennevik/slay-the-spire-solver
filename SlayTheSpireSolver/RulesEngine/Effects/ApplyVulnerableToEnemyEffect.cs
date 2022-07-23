using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Debuffs;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ApplyVulnerableToEnemyEffect(EnemyId Target, Vulnerable VulnerableToApply) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState.ModifyEnemy(Target, enemy =>
            enemy with { Vulnerable = enemy.Vulnerable.Add(VulnerableToApply) });
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
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { Id = EnemyId.New() } } };
        var effect = new ApplyVulnerableToEnemyEffect(EnemyId.Default, new Vulnerable(1));
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void AppliesVulnerableToTargetEnemy(int vulnerableAmount)
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { Turn = 3, EnemyParty = new[] { targetEnemy } };
        var effect = new ApplyVulnerableToEnemyEffect(targetEnemy.Id, vulnerableAmount);
        var result = effect.Resolve(gameState).Single().GameState;
        var expectedGameState = gameState with
        {
            EnemyParty = new[] { targetEnemy with { Vulnerable = vulnerableAmount } }
        };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void AddsVulnerableToEnemyWithVulnerable(int vulnerableAmount)
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), Vulnerable = 2 };
        var gameState = new GameState { Turn = 3, EnemyParty = new[] { targetEnemy } };
        var effect = new ApplyVulnerableToEnemyEffect(targetEnemy.Id, vulnerableAmount);
        var result = effect.Resolve(gameState).Single().GameState;
        var expectedGameState = gameState with
        {
            EnemyParty = new[] { targetEnemy with { Vulnerable = 2 + vulnerableAmount } }
        };
        Assert.AreEqual(expectedGameState, result);
    }
}