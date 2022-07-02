using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture(typeof(ResolveEnemyMoveEffect))]
[TestFixture(typeof(ChooseNewEnemyMoveEffect))]
public class ResolveForAllEnemiesEffectTestBase<T> where T : TargetEnemyEffect, new()
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState();
        var effect = new ResolveForAllEnemiesEffect<T>();
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ResolvesSingleEnemyEffect()
    {
        var enemy = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveForAllEnemiesEffect<T>();
        var result = effect.Resolve(gameState).SingleUnresolvedState();
        Assert.AreEqual(gameState.WithEffects(new EffectStack(new T { Target = enemy.Id })), result);
    }

    [Test]
    public void ResolvesMultipleEnemyEffectsInOrder()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy1, enemy2) };
        var effect = new ResolveForAllEnemiesEffect<T>();
        var result = effect.Resolve(gameState).SingleUnresolvedState();
        Assert.AreEqual(
            gameState.WithEffects(new EffectStack(new T { Target = enemy2.Id }, new T { Target = enemy1.Id })),
            result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new ResolveForAllEnemiesEffect<T>(), new ResolveForAllEnemiesEffect<T>());
    }
}