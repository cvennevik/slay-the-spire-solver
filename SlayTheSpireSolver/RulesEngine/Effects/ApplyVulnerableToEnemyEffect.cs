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
public class ApplyVulnerableToEnemyEffectTests
{
    [Test]
    public void Test()
    {
        Assert.True(true);
    }
}