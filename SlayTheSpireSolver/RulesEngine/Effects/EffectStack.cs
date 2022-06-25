namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly struct EffectStack
{
    private readonly IEffect[] _effects;

    public EffectStack() : this(Array.Empty<IEffect>()) { }

    public EffectStack(params IEffect[] effects)
    {
        _effects = effects;
    }

    public EffectStack Push(IEffect effect)
    {
        return new EffectStack(_effects.Append(effect).ToArray());
    }

    public EffectStack Push(EffectStack addedEffectStack)
    {
        return new EffectStack(_effects.Concat(addedEffectStack._effects).ToArray());
    }

    public (IEffect, EffectStack) Pop()
    {
        if (_effects.Length == 0)
        {
            return (new NullEffect(), this);
        }

        return (_effects[^1], new EffectStack(_effects.Take(_effects.Length - 1).ToArray()));
    }

    public override bool Equals(object? obj)
    {
        return obj is EffectStack effectStack && _effects.SequenceEqual(effectStack._effects);
    }

    public override int GetHashCode()
    {
        return _effects.GetHashCode();
    }

    public static bool operator ==(EffectStack left, EffectStack right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(EffectStack left, EffectStack right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"[{string.Join<IEffect>(", ", _effects)}]";
    }
}