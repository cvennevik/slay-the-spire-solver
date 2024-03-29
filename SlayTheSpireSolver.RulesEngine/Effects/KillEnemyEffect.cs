using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect(EnemyId TargetId) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(TargetId)) return gameState;
        var newEnemyParty = gameState.EnemyParty.Remove(TargetId);
        var newGameState = gameState with { EnemyParty = newEnemyParty };
        return newEnemyParty.Any() ? newGameState : newGameState.WithAddedEffects(new EndCombatEffect());
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
            EffectStack = new EffectStack(new NullEffect())
        };
        var effect = new KillEnemyEffect(EnemyId.Default);
        var expectedGameState = new GameState
        {
            EffectStack = new EffectStack(new NullEffect(), new EndCombatEffect())
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void KillsOnlyTargetEnemyAndDoesNotEndCombat()
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