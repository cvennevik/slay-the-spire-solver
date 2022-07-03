using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Debuffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Bash : TargetedCard
{
    public override Energy GetCost() => 2;

    public override EffectStack GetTargetedEffect(EnemyId target) => new Effect[]
    {
        new ApplyVulnerableToEnemyEffect(target, new Vulnerable(2)),
        new AttackEnemyEffect(target, new Damage(8))
    };

    protected override string GetName() => "Bash";
}

[TestFixture]
internal class BashTests : TargetedCardTests<Bash> { }