using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackEnemyEffect(EnemyId Target, Damage Damage) : Effect
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
        var gameState = new GameState { Turn = 3 };
        var effect = new AttackEnemyEffect(EnemyId.Default, new Damage(1));
    }
}