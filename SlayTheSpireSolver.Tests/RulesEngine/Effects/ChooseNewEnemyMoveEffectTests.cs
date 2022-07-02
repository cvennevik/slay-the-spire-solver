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
        var result = effect.Resolve(gameState);
    }
}