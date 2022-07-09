using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect(EnemyId TargetId) : Effect
{
    public override PossibilitySet NewResolve(GameState gameState)
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
        Assert.AreEqual(new GameState(), effect.NewResolve(gameState).Single().GameState);
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
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Id = id1 }, new JawWorm { Id = id3 })
        };
        Assert.AreEqual(expectedGameState, effect.NewResolve(gameState).Single().GameState);
    }

    [Test]
    public void DoesNothingIfNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new KillEnemyEffect(EnemyId.New());
        Assert.AreEqual(gameState, effect.NewResolve(gameState).Single().GameState);
    }
}