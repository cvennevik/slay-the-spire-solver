using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ClearAllEnemyArmorEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty() };
        var effect = new ClearAllEnemyArmorEffect();
    }
}