using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ResolveEnemyMoveEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty() };
    }

    [Test]
    public void EqualityTest()
    {
        Assert.AreEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.Default));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.New()));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.New()), new ResolveEnemyMoveEffect(EnemyId.New()));
    }
}