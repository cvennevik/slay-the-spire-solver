using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveForAllEnemiesEffect<T> : Effect where T : TargetEnemyEffect, new()
{
    public override ResolvablePossibilitySet OldResolve(GameState gameState)
    {
        var resolveEnemyMoveEffects =
            gameState.EnemyParty.Select(enemy => new T {Target = enemy.Id}).Reverse();
        return gameState.WithEffects(new EffectStack(resolveEnemyMoveEffects));
    }
}

[TestFixture]
internal class ResolveForAllEnemiesEffectTests
{
    [TestFixture]
    internal class ResolveAllEnemyMovesEffectTest : ResolveForAllEnemiesEffectTestBase<ResolveEnemyMoveEffect> { }

    [TestFixture]
    internal class ChooseAllNewEnemyMovesEffectTest : ResolveForAllEnemiesEffectTestBase<ChooseNewEnemyMoveEffect> { }

    [TestFixture]
    internal class DecreaseAllEnemiesVulnerableEffectTest :
        ResolveForAllEnemiesEffectTestBase<DecreaseEnemyVulnerableEffect> { }
}

internal abstract class ResolveForAllEnemiesEffectTestBase<T> where T : TargetEnemyEffect, new()
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState();
        var effect = new ResolveForAllEnemiesEffect<T>();
        var result = effect.NewResolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ResolvesSingleEnemyEffect()
    {
        var enemy = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveForAllEnemiesEffect<T>();
        var result = effect.OldResolve(gameState).SingleUnresolvedState();
        Assert.AreEqual(gameState.WithEffects(new EffectStack(new T { Target = enemy.Id })), result);
    }

    [Test]
    public void ResolvesMultipleEnemyEffectsInOrder()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy1, enemy2) };
        var effect = new ResolveForAllEnemiesEffect<T>();
        var result = effect.OldResolve(gameState).SingleUnresolvedState();
        Assert.AreEqual(
            gameState.WithEffects(new EffectStack(new T { Target = enemy2.Id }, new T { Target = enemy1.Id })),
            result);
    }
}