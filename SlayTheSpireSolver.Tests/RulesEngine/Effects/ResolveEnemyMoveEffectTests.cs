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
    public void Test()
    {
        var enemy = new JawWorm { IntendedMove = new Chomp() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveEnemyMoveEffect(enemy.Id);
        var result = effect.Resolve(gameState).Single();
        var expectedEffectStack = enemy.GetMoveEffects();
    }

    [Test]
    public void EqualityTest()
    {
        Assert.AreEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.Default));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.New()));
        Assert.AreNotEqual(new ResolveEnemyMoveEffect(EnemyId.New()), new ResolveEnemyMoveEffect(EnemyId.New()));
    }
}