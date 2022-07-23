using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DecreaseEnemyVulnerableEffect : TargetEnemyEffect
{
    public DecreaseEnemyVulnerableEffect() { }
    public DecreaseEnemyVulnerableEffect(EnemyId enemyId) : base(enemyId) { }

    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState.EnemyParty.Has(Target)
            ? gameState.ModifyEnemy(Target, enemy => enemy with { Vulnerable = enemy.Vulnerable.Decrease() })
            : gameState;
    }
}

[TestFixture]
internal class DecreaseEnemyVulnerableEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { Id = EnemyId.New(), Vulnerable = 2 } } };
        var effect = new DecreaseEnemyVulnerableEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ReducesVulnerableOfTargetEnemy()
    {
        var gameState = new GameState { Turn = 2, EnemyParty = new[] { new JawWorm { Vulnerable = 2 } } };
        var effect = new DecreaseEnemyVulnerableEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState with { EnemyParty = new[] { new JawWorm { Vulnerable = 1 } } }, result);
    }

    [Test]
    public void DoesNothingIfVulnerableAlreadyZero()
    {
        var gameState = new GameState { Turn = 2, EnemyParty = new[] { new JawWorm { Vulnerable = 0 } } };
        var effect = new DecreaseEnemyVulnerableEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void OnlyReducesVulnerableOfTargetEnemy()
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), Vulnerable = 3 };
        var otherEnemy = new JawWorm { Id = EnemyId.New(), Vulnerable = 2 };
        var gameState = new GameState { Turn = 2, EnemyParty = new[] { targetEnemy, otherEnemy } };
        var effect = new DecreaseEnemyVulnerableEffect(targetEnemy.Id);
        var result = effect.Resolve(gameState).Single().GameState;
        var expectedResult = gameState with { EnemyParty = new[] { targetEnemy with { Vulnerable = 2 }, otherEnemy } };
        Assert.AreEqual(expectedResult, result);
    }
}