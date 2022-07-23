using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly struct EffectStack
{
    private readonly Effect[] _effects;

    public bool IsEmpty() => _effects.Length == 0;

    public EffectStack() : this(Array.Empty<Effect>()) { }
    
    public static implicit operator EffectStack(Effect effect) => new(effect);
    public static implicit operator EffectStack(Effect[] effects) => new(effects);

    public EffectStack(IEnumerable<Effect> effects) : this(effects.ToArray()) { }

    public EffectStack(params Effect[] effects)
    {
        _effects = effects;
    }

    public EffectStack Push(Effect effect)
    {
        return new EffectStack(_effects.Append(effect).ToArray());
    }

    public EffectStack Push(EffectStack addedEffectStack)
    {
        return new EffectStack(_effects.Concat(addedEffectStack._effects).ToArray());
    }

    public (Effect, EffectStack) Pop()
    {
        return _effects.Length == 0
            ? (new NullEffect(), this)
            : (_effects[^1], new EffectStack(_effects.Take(_effects.Length - 1).ToArray()));
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
        return $"[{string.Join<Effect>(", ", _effects)}]";
    }
}

[TestFixture]
internal class EffectStackTests
{
    [Test]
    public void TestEquality()
    {
        var effect1 = new RemoveEnergyEffect(1);
        var effect2 = new RemoveEnergyEffect(2);
        Assert.AreEqual(new EffectStack(), new EffectStack());
        Assert.AreEqual(new EffectStack(effect1), new EffectStack(effect1));
        Assert.AreEqual(new EffectStack(effect2), new EffectStack(effect2));
        Assert.AreEqual(new EffectStack(effect1, effect1), new EffectStack(effect1, effect1));
        Assert.AreNotEqual(new EffectStack(effect1, effect2), new EffectStack(effect2, effect1));
        Assert.AreNotEqual(new EffectStack(effect1, effect2), new EffectStack(effect1));
    }

    [Test]
    public void TestPushEffect()
    {
        var effect1 = new RemoveEnergyEffect(1);
        var effect2 = new RemoveEnergyEffect(2);
        Assert.AreEqual(new EffectStack(effect1), new EffectStack().Push(effect1));
        Assert.AreEqual(new EffectStack(effect1, effect2), new EffectStack(effect1).Push(effect2));
    }

    [Test]
    public void TestPushEffectStack()
    {
        var effectStack1 = new EffectStack();
        var effectStack2 = new EffectStack(new RemoveEnergyEffect(1));
        Assert.AreEqual(new EffectStack(new RemoveEnergyEffect(1)), effectStack1.Push(effectStack2));
    }

    [Test]
    public void TestPop()
    {
        var effect1 = new RemoveEnergyEffect(1);
        var effect2 = new RemoveEnergyEffect(2);
        Assert.AreEqual((effect2, new EffectStack(effect1)), new EffectStack(effect1, effect2).Pop());
        Assert.AreEqual((effect1, new EffectStack()), new EffectStack(effect1).Pop());
        Assert.AreEqual((new NullEffect(), new EffectStack()), new EffectStack().Pop());
    }
}