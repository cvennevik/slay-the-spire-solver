using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackEnemyEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        throw new NotImplementedException();
    }
}

[TestFixture]
public class AttackEnemyEffectTests
{

}