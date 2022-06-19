using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamagePlayerEffect : IEffect
{
    public Damage Damage { get; }

    public DamagePlayerEffect(Damage damage)
    {
        Damage = damage;
    }

    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        throw new NotImplementedException();
    }
}