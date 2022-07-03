using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Defend : UntargetedCard
{
    protected override string GetName() => "Defend";
    public override Energy GetCost() => 1;
    public override Effect GetEffect() => new GainPlayerArmorEffect(5);
}

[TestFixture]
internal class DefendTests : UntargetedCardTests<Defend> { }