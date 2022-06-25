using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class EffectStackTests
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