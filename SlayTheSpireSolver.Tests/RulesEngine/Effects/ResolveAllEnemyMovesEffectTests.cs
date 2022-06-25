using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ResolveAllEnemyMovesEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState();
        var effect = new ResolveAllEnemyMovesEffect();
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ResolvesSingleEnemyEffect()
    {
        var enemy = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveAllEnemyMovesEffect();
        var result = effect.Resolve(gameState).Single();
        Assert.AreEqual(gameState.WithEffects(new EffectStack(new ResolveEnemyMoveEffect(enemy.Id))), result);
    }

    [Test]
    public void ResolvesMultipleEnemyEffectsInOrder()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy1, enemy2) };
        var effect = new ResolveAllEnemyMovesEffect();
        var result = effect.Resolve(gameState).Single();
        Assert.AreEqual(gameState.WithEffects(new EffectStack(new ResolveEnemyMoveEffect(enemy2.Id), new ResolveEnemyMoveEffect(enemy1.Id))), result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new ResolveAllEnemyMovesEffect(), new ResolveAllEnemyMovesEffect());
    }
}