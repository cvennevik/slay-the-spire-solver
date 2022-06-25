using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;
using SlayTheSpireSolver.Tests.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class ActionTests
{
    // TODO: Fix probability
    
    [Test]
    public void ResolvesZeroEffects()
    {
        var actionWithEffectStack = new Action(new GameState(), new EffectStack());
        var resolvedState = actionWithEffectStack.Resolve().SingleResolvedState();
        Assert.AreEqual(new GameState(), resolvedState);
    }

    [Test]
    public void ResolvesOneEffect()
    {
        var gameState = new GameState { PlayerArmor = 0 };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(5));
        var actionWithEffectStack = new Action(gameState, effectStack);
        var resolvedState = actionWithEffectStack.Resolve().SingleResolvedState();
        Assert.AreEqual(new GameState { PlayerArmor = 5 }, resolvedState);
    }

    [Test]
    public void ResolvesTwoEffects()
    {
        var gameState = new GameState { Energy = 2, PlayerArmor = 0 };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(5), new RemoveEnergyEffect(1));
        var actionWithEffectStack = new Action(gameState, effectStack);
        var resolvedState = actionWithEffectStack.Resolve().SingleResolvedState();
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
        var actionWithEffectStack = new Action(gameState, effectStack);
        var resolvedState = actionWithEffectStack.Resolve().SingleResolvedState();
        var expectedGameState = gameState with { PlayerHealth = 6 };
        Assert.AreEqual(expectedGameState, resolvedState);
    }

    [Test]
    public void ResolvesEffectWithMultipleOutcomes()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(), new Defend())
        };
        var effectStack = new EffectStack(new DrawCardEffect());
        var actionWithEffectStack = new Action(gameState, effectStack);
        var result = actionWithEffectStack.Resolve();
        var expectedResult1 = new GameState
        {
            Hand = new Hand(new Strike()),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Defend())
        };
        var expectedResult2 = new GameState
        {
            Hand = new Hand(new Defend()),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike())
        };
        Assert.AreEqual(2, result.Count());
        Assert.Contains(expectedResult1.WithProbability(1),result.ToList());
        Assert.Contains(expectedResult2.WithProbability(1),result.ToList());
    }

    [Test]
    public void ResolvesMultipleEffectWithMultipleOutcomes()
    {
        var gameState = new GameState
        {
            Hand = new Hand(), DrawPile = new DrawPile(new Strike(), new Strike(), new Defend())
        };
        var effectStack = new EffectStack(new DrawCardEffect(), new DrawCardEffect());
        var actionWithEffectStack = new Action(gameState, effectStack);
        var result = actionWithEffectStack.Resolve();
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
        Assert.AreEqual(2, result.Count());
        Assert.Contains(expectedResult1.WithProbability(1), result.ToList());
        Assert.Contains(expectedResult2.WithProbability(1), result.ToList());
    }

    [Test]
    public void EqualityTests()
    {
        var gameState1 = new GameState { Turn = 1 };
        var gameState2 = new GameState { Turn = 2 };
        var effectStack1 = new EffectStack(new RemoveEnergyEffect(1));
        var effectStack2 = new EffectStack(new RemoveEnergyEffect(2));
        Assert.AreEqual(new Action(gameState1, effectStack1),
            new Action(gameState1, effectStack1));
        Assert.AreEqual(new Action(gameState2, effectStack2),
            new Action(gameState2, effectStack2));
        Assert.AreNotEqual(new Action(gameState1, effectStack1),
            new Action(gameState1, effectStack2));
        Assert.AreNotEqual(new Action(gameState1, effectStack1),
            new Action(gameState2, effectStack1));
    }
}