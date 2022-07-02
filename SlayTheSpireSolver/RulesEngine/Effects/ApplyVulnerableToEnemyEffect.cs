using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ApplyVulnerableToEnemyEffect : Effect
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
        var effect = new ApplyVulnerableToEnemyEffect();
    }
}