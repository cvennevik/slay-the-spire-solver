using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Strike : TargetedCard
{
    protected override string GetName() => "Strike";
    public override Energy GetCost() => 1;
    public override Effect GetTargetedEffect(EnemyId target) => new AttackEnemyEffect(target, 6);
}


[TestFixture]
internal class StrikeTests : TargetedCardTests<Strike> { }