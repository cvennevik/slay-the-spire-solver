using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Debuffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Bash : TargetedCard
{
    private readonly Damage _damage = 8;
    private readonly Vulnerable _vulnerable = 2;

    public override Energy GetCost() => 2;

    public override EffectStack GetTargetedEffect(EnemyId target) => new Effect[]
    {
        new ApplyVulnerableToEnemyEffect(target, _vulnerable),
        new AttackEnemyEffect(target, _damage)
    };

    protected override string GetName() => "Bash";
}

[TestFixture]
internal class BashTests : TargetedCardTests<Bash> { }