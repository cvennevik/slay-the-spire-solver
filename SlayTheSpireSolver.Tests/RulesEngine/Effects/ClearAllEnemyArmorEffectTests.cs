using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ClearAllEnemyArmorEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty() };
        var effect = new ClearAllEnemyArmorEffect();
        var result = effect.Resolve(gameState).SingleStableGameState();
        Assert.AreEqual(gameState, result);
    }
}