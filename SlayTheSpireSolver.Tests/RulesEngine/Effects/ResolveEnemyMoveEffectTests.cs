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
    public void Test()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() }) };
        var effect = new ResolveEnemyMoveEffect(EnemyId.Default);
        var result = effect.Resolve(gameState).AsSingleStableGameState();
    }

    [Test]
    public void EqualityTest()
    {
        Assert.AreEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.Default));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.New()));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.New()), new ResolveEnemyMoveEffect(EnemyId.New()));
    }
}