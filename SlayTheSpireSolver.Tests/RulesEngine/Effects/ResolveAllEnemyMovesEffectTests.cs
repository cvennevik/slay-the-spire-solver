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
        var result = effect.Resolve(gameState).SingleStableGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ResolvesSingleEnemyEffect()
    {
        var enemy = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveAllEnemyMovesEffect();
        var result = effect.Resolve(gameState).Single();
    }
}