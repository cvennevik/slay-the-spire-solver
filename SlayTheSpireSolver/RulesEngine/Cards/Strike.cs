using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Strike : TargetedCard
{
    public override Energy GetCost() => 1;
    public override Effect GetEffect(EnemyId target) => new AttackEnemyEffect(target, 6);
    public override string ToString() => "Strike";
}


[TestFixture]
internal class StrikeTests : TargetedCardTests<Strike> { }