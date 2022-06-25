using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ResolveEnemyMoveEffectTests
{
    [Test]
    public void PutsEnemyMoveEffectsOnEffectStack()
    {
        var enemy = new JawWorm { IntendedMove = new Chomp() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveEnemyMoveEffect(enemy.Id);
        var result = effect.Resolve(gameState).Single();
        var expectedEffectStack = enemy.GetMoveEffects();
        Assert.AreEqual(gameState.AsResolvable(expectedEffectStack), result);
    }

    [Test]
    public void OnlyPlacesEffectsOfTargetEnemyOnEffectStack()
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() };
        var otherEnemy = new JawWorm { Id = EnemyId.New(), IntendedMove = new Thrash() };
        var gameState = new GameState { EnemyParty = new EnemyParty(targetEnemy, otherEnemy) };
        var effect = new ResolveEnemyMoveEffect(targetEnemy.Id);
        var result = effect.Resolve(gameState).Single();
        var expectedEffectStack = targetEnemy.GetMoveEffects();
        Assert.AreEqual(gameState.AsResolvable(expectedEffectStack), result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithId()
    {
        var enemy = new JawWorm { IntendedMove = new Chomp() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveEnemyMoveEffect(EnemyId.New());
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void EqualityTest()
    {
        Assert.AreEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.Default));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.New()));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.New()), new ResolveEnemyMoveEffect(EnemyId.New()));
    }
}