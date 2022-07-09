using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record ResolvableGameState(GameState GameState, EffectStack EffectStack)
{
    // GOAL: Add EffectStack to GameState, get rid of this class
    // PLAN:
    //  1. Add EffectStack to GameState
    //  2. Replace ResolvableGameState use with GameState use

    public IReadOnlyList<Possibility> Resolve()
    {
        return WithProbability(1).Resolve();
    }

    public ResolvablePossibility WithProbability(Probability probability) => new(this, probability);
}

[TestFixture]
internal class ResolvableGameStateTests
{    
    [Test]
    public void ResolvesZeroEffects()
    {
        var action = new ResolvableGameState(new GameState(), new EffectStack());
        var resolvedState = action.Resolve().Single().GameState;
        Assert.AreEqual(new GameState(), resolvedState);
    }

    [Test]
    public void ResolvesOneEffect()
    {
        var gameState = new GameState { PlayerArmor = 0 };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(5));
        var action = new ResolvableGameState(gameState, effectStack);
        var resolvedState = action.Resolve().Single().GameState;
        Assert.AreEqual(new GameState { PlayerArmor = 5 }, resolvedState);
    }

    [Test]
    public void ResolvesTwoEffects()
    {
        var gameState = new GameState { Energy = 2, PlayerArmor = 0 };
        var effectStack = new EffectStack(new GainPlayerArmorEffect(5), new RemoveEnergyEffect(1));
        var action = new ResolvableGameState(gameState, effectStack);
        var resolvedState = action.Resolve().Single().GameState;
        Assert.AreEqual(new GameState { Energy = 1, PlayerArmor = 5 }, resolvedState);
    }

    [Test]
    public void ResolvesEffectThatAddNewEffects()
    {
        var gameState = new GameState
        {
            PlayerHealth = 30,
            EnemyParty = new EnemyParty(new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() },
                new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() })
        };
        var effectStack = new EffectStack(new ResolveForAllEnemiesEffect<ResolveEnemyMoveEffect>());
        var action = new ResolvableGameState(gameState, effectStack);
        var resolvedState = action.Resolve().Single().GameState;
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
        var action = new ResolvableGameState(gameState, effectStack);
        var result = action.Resolve();
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
        Assert.AreEqual(2, result.Count);
        Assert.Contains(expectedResult1.WithProbability(0.75),result.ToList());
        Assert.Contains(expectedResult2.WithProbability(0.25),result.ToList());
    }

    [Test]
    public void ResolvesMultipleEffectsWithMultipleOutcomes()
    {
        var gameState = new GameState
        {
            Hand = new Hand(),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike(), new Strike(), new Defend())
        };
        var effectStack = new EffectStack(new DrawCardEffect(), new DrawCardEffect());
        var action = new ResolvableGameState(gameState, effectStack);
        var result = action.Resolve();
        var expectedResult1 = new GameState
        {
            Hand = new Hand(new Strike(), new Strike()),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Defend())
        }.WithProbability(0.6);
        var expectedResult2 = new GameState
        {
            Hand = new Hand(new Strike(), new Defend()),
            DrawPile = new DrawPile(new Strike(), new Strike(), new Strike())
        }.WithProbability(0.4);
        const double tolerance = 0.000000000000001;
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedResult1, tolerance)));
        Assert.AreEqual(1, result.Count(x => x.IsEqualTo(expectedResult2, tolerance)));
    }
}