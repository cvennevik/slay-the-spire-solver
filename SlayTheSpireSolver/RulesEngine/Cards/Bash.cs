using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Bash : TargetedCard
{
    public override Energy GetCost() => 2;

    public override Effect GetTargetedEffect(EnemyId target)
    {
        throw new NotImplementedException();
    }

    protected override string GetName() => "Bash";
}