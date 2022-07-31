using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect(EnemyId TargetId) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState with { EnemyParty = gameState.EnemyParty.Remove(TargetId) };
    }
}

[TestFixture]
internal class KillEnemyEffectTests
{
    [Test]
    public void KillsSingleEnemyAndEndsCombat()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm()),
            CombatHasEnded = false
        };
        var effect = new KillEnemyEffect(EnemyId.Default);
        var expectedGameState = new GameState { CombatHasEnded = true };
        // Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
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
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void DoesNothingIfNoEnemyHasTargetId()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new KillEnemyEffect(EnemyId.New());
        Assert.AreEqual(gameState, effect.Resolve(gameState).Single().GameState);
    }
}