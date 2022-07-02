using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ChooseNewEnemyMoveEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } } };
        var effect = new ChooseNewEnemyMoveEffect(EnemyId.New());
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void AddsIntendedMoveToPreviousMovesAndSetsNewIntendedMove()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } } };
        var effect = new ChooseNewEnemyMoveEffect(EnemyId.Default);
        var result = effect.Resolve(gameState);
        var bellowEnemy = new JawWorm { IntendedMove = new Bellow(), PreviousMoves = new[] { new Chomp() } };
        var thrashEnemy = new JawWorm { IntendedMove = new Thrash(), PreviousMoves = new[] { new Chomp() } };
        var expectedPossibilities = new ResolvablePossibility[]
        {
            (gameState with { EnemyParty = new[] { bellowEnemy } }).WithProbability(0.45 / 0.75),
            (gameState with { EnemyParty = new[] { thrashEnemy } }).WithProbability(0.3 / 0.75)
        };
        Assert.That(result, Is.EquivalentTo(expectedPossibilities));
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new ChooseNewEnemyMoveEffect(EnemyId.Default), new ChooseNewEnemyMoveEffect(EnemyId.Default));
    }
}