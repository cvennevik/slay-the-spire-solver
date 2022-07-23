using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect(EnemyId TargetId) : Effect
{
    public virtual PossibilitySet Resolve(GameState gameState)
    {
        return gameState with { EnemyParty = gameState.EnemyParty.Remove(TargetId) };
    }
}

[TestFixture]
internal class KillEnemyEffectTests
{
    [Test]
    public void KillsSingleEnemy()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new KillEnemyEffect(EnemyId.Default);
        Assert.AreEqual(new GameState(), effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void KillsOnlyTargetEnemy()
    {
        var (id1, id2, id3) = (new EnemyId(), new EnemyId(), new EnemyId());
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = id1 }, new JawWorm { Id = id2 }, new JawWorm { Id = id3 })
        };
        var effect = new KillEnemyEffect(id2);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = id1 }, new JawWorm { Id = id3 })
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void DoesNothingIfNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new KillEnemyEffect(new EnemyId());
        Assert.AreEqual(gameState, effect.Resolve(gameState).Single().GameState);
    }
}