using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ResolveAllEnemyMovesEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState();
        var effect = new ResolveAllEnemyMovesEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
    }
}