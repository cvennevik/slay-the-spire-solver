using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ChooseNewEnemyMoveEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { IntendedMove = new Chomp() } } };
    }
}