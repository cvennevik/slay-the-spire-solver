using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class ActionWithEffectStackTests
{
    [Test]
    public void ResolvesZeroEffects()
    {
        var actionWithEffectStack = new ActionWithEffectStack(new GameState(), new EffectStack());
        var resolvedState = actionWithEffectStack.ResolveToPossibleStates().Single();
        Assert.AreEqual(new GameState(), resolvedState);
    }

    [Test]
    public void ResolvesOneEffect()
    {
        var gameState = new GameState { PlayerArmor = new Armor(0) };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(new Armor(5)));
        var actionWithEffectStack = new ActionWithEffectStack(gameState, effectStack);
        var resolvedState = actionWithEffectStack.ResolveToPossibleStates().Single();
        Assert.AreEqual(new GameState { PlayerArmor = new Armor(5) }, resolvedState);
    }

    [Test]
    public void ResolvesTwoEffects()
    {
        var gameState = new GameState { Energy = new Energy(2), PlayerArmor = new Armor(0) };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(new Armor(5)), new RemoveEnergyEffect(new Energy(1)));
        var actionWithEffectStack = new ActionWithEffectStack(gameState, effectStack);
        var resolvedState = actionWithEffectStack.ResolveToPossibleStates().Single();
        Assert.AreEqual(new GameState { Energy = new Energy(1), PlayerArmor = new Armor(5) }, resolvedState);
    }

    [Test]
    public void EqualityTests()
    {
        var gameState1 = new GameState { Turn = new Turn(1) };
        var gameState2 = new GameState { Turn = new Turn(2) };
        var effectStack1 = new EffectStack(new RemoveEnergyEffect(new Energy(1)));
        var effectStack2 = new EffectStack(new RemoveEnergyEffect(new Energy(2)));
        Assert.AreEqual(new ActionWithEffectStack(gameState1, effectStack1),
            new ActionWithEffectStack(gameState1, effectStack1));
        Assert.AreEqual(new ActionWithEffectStack(gameState2, effectStack2),
            new ActionWithEffectStack(gameState2, effectStack2));
        Assert.AreNotEqual(new ActionWithEffectStack(gameState1, effectStack1),
            new ActionWithEffectStack(gameState1, effectStack2));
        Assert.AreNotEqual(new ActionWithEffectStack(gameState1, effectStack1),
            new ActionWithEffectStack(gameState2, effectStack1));
    }
}