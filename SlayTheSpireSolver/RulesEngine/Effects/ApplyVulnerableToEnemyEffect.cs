using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ApplyVulnerableToEnemyEffect(EnemyId Target) : TargetEnemyEffect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}

[TestFixture]
internal class ApplyVulnerableToEnemyEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { Turn = 3 };
        var effect = new ApplyVulnerableToEnemyEffect(EnemyId.Default);
    }
}