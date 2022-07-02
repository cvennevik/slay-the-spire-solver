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
    public void Test()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } } };
        var effect = new ChooseNewEnemyMoveEffect(EnemyId.Default);
        var result = effect.Resolve(gameState);
    }
}