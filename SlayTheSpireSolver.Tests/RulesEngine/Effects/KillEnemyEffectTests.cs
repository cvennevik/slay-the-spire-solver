using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class KillEnemyEffectTests
{
    [Test]
    public void KillsSingleEnemy()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new KillEnemyEffect(EnemyId.Default);
        Assert.AreEqual(new GameState(), effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void KillsOnlyTargetEnemy()
    {
        var (id1, id2, id3) = (EnemyId.New(), EnemyId.New(), EnemyId.New());
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = id1 }, new JawWorm { Id = id2 }, new JawWorm { Id = id3 })
        };
        var effect = new KillEnemyEffect(id2);
        Assert.AreEqual(
            new GameState { EnemyParty = new EnemyParty(new JawWorm { Id = id1 }, new JawWorm { Id = id3 }) },
            effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void DoesNothingIfNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new KillEnemyEffect(EnemyId.New());
        Assert.AreEqual(gameState, effect.Resolve(gameState).SingleResolvedGameState());
    }
}