using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackEnemyEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        return gameState;
    }
}

[TestFixture]
public class AttackEnemyEffectTests
{
    [Test]
    public void Test()
    {
        
    }
}