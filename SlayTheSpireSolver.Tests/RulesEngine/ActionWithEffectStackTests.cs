using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
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
        var gameState = new GameState { PlayerArmor = 0 };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(5));
        var actionWithEffectStack = new ActionWithEffectStack(gameState, effectStack);
        var resolvedState = actionWithEffectStack.ResolveToPossibleStates().Single();
        Assert.AreEqual(new GameState { PlayerArmor = 5 }, resolvedState);
    }

    [Test]
    public void ResolvesTwoEffects()
    {
        var gameState = new GameState { Energy = 2, PlayerArmor = 0 };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(5), new RemoveEnergyEffect(1));
        var actionWithEffectStack = new ActionWithEffectStack(gameState, effectStack);
        var resolvedState = actionWithEffectStack.ResolveToPossibleStates().Single();
        Assert.AreEqual(new GameState { Energy = 1, PlayerArmor = 5 }, resolvedState);
    }

    [Test]
    public void ResolvesEffectThatAddNewEffects()
    {
        var gameState = new GameState
        {
            PlayerHealth = 30,
            EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() },
                new JawWorm { IntendedMove = new Chomp() })
        };
        var effectStack = new EffectStack(new ResolveAllEnemyMovesEffect());
        var actionWithEffectStack = new ActionWithEffectStack(gameState, effectStack);
        var resolvedState = actionWithEffectStack.ResolveToPossibleStates().Single();
        var expectedGameState = gameState with { PlayerHealth = 6 };
        Assert.AreEqual(expectedGameState, resolvedState);
    }

    [Test]
    public void ResolvesEffectWithMultipleOutcomes()
    {
        var gameState = new GameState
        {
            Hand = new Hand(), DrawPile = new DrawPile(new Strike(), new Strike(), new Defend())
        };
        var effectStack = new EffectStack(new DrawCardEffect());
        var actionWithEffectStack = new ActionWithEffectStack(gameState, effectStack);
        var result = actionWithEffectStack.ResolveToPossibleStates();
        var expectedResult1 = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Defend())
        };
        var expectedResult2 = new GameState
        {
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike(), new Strike())
        };
        Assert.AreEqual(2, result.Count);
        Assert.Contains(expectedResult1,result.ToList());
        Assert.Contains(expectedResult2,result.ToList());
    }

    [Test]
    public void ResolvesMultipleEffectWithMultipleOutcomes()
    {
        var gameState = new GameState
        {
            Hand = new Hand(), DrawPile = new DrawPile(new Strike(), new Strike(), new Defend())
        };
        var effectStack = new EffectStack(new DrawCardEffect(), new DrawCardEffect());
        var actionWithEffectStack = new ActionWithEffectStack(gameState, effectStack);
        var result = actionWithEffectStack.ResolveToPossibleStates();
        var expectedResult1 = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DrawPile = new DrawPile(new Defend())
        };
        var expectedResult2 = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile(new Strike())
        };
        Assert.AreEqual(2, result.Count);
        Assert.Contains(expectedResult1, result.ToList());
        Assert.Contains(expectedResult2, result.ToList());
    }

    [Test]
    public void EqualityTests()
    {
        var gameState1 = new GameState { Turn = 1 };
        var gameState2 = new GameState { Turn = 2 };
        var effectStack1 = new EffectStack(new RemoveEnergyEffect(1));
        var effectStack2 = new EffectStack(new RemoveEnergyEffect(2));
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