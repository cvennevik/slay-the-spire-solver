using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ApplyVulnerableToEnemyEffect(EnemyId Default) : TargetEnemyEffect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        // TODO
        throw new NotImplementedException();
    }
}

[TestFixture]
internal class ApplyVulnerableToEnemyEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState();
        var effect = new ApplyVulnerableToEnemyEffect(EnemyId.Default);
    }
}