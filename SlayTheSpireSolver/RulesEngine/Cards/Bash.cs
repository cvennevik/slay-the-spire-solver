using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Bash : TargetedCard
{
    public override Energy GetCost()
    {
        throw new NotImplementedException();
    }

    protected override string GetName()
    {
        throw new NotImplementedException();
    }

    public override Effect GetTargetedEffect(EnemyId target)
    {
        throw new NotImplementedException();
    }
}