using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ResolveEnemyMoveEffectTests
{
    [Test]
    public void EqualityTest()
    {
        Assert.AreEqual(new ResolveEnemyMoveEffect(EnemyId.Default), new ResolveEnemyMoveEffect(EnemyId.Default));
    }
}