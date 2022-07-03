using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Defend : UntargetedCard
{
    public override Energy GetCost() => 1;
    public override Effect GetEffect() => new GainPlayerArmorEffect(5);
    public override string GetName() => "Defend";
}

[TestFixture]
internal class DefendTests : UntargetedCardTests<Defend> { }