namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly struct CombinedEffect : IEffect
{
    private readonly IEffect[] _effects;

    public CombinedEffect() : this(Array.Empty<IEffect>())
    {
    }

    public CombinedEffect(params IEffect[] effects)
    {
        _effects = effects;
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        IEnumerable<GameState> interimGameStates = new[] { gameState };

        foreach (var effect in _effects)
        {
            interimGameStates = interimGameStates.SelectMany(effect.ApplyTo);
        }

        return interimGameStates.ToArray();
    }

    public override bool Equals(object? obj)
    {
        return obj is CombinedEffect otherEffect
               && UnnestEffects(_effects).SequenceEqual(UnnestEffects(otherEffect._effects));
    }

    private static IEnumerable<IEffect> UnnestEffects(IEffect[] effects)
    {
        while (effects.Any(effect => effect is CombinedEffect))
        {
            effects = effects.SelectMany(effect =>
                    effect is CombinedEffect combinedEffect
                        ? combinedEffect._effects
                        : new[] { effect })
                .ToArray();
        }

        return effects;
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public static bool operator ==(CombinedEffect left, CombinedEffect right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CombinedEffect left, CombinedEffect right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"{nameof(CombinedEffect)}: {{ {string.Join<IEffect>(", ", _effects)} }}";
    }
}