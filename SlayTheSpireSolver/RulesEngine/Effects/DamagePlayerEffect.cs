using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamagePlayerEffect
{
    public Damage Damage { get; }

    public DamagePlayerEffect(Damage damage)
    {
        Damage = damage;
    }
}