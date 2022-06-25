using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

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
}