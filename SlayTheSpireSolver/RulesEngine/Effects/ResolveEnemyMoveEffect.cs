using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ResolveEnemyMoveEffect : TargetEnemyEffect
{
    public ResolveEnemyMoveEffect() { }
    public ResolveEnemyMoveEffect(EnemyId target) : base(target) { }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;
        var enemyMoveEffects = gameState.EnemyParty.Get(Target).GetMoveEffects();
        return gameState.WithEffects(enemyMoveEffects);
    }
}

[TestFixture]
internal class ResolveEnemyMoveEffectTests
{
    [Test]
    public void PutsEnemyMoveEffectsOnEffectStack()
    {
        var enemy = new JawWorm { IntendedMove = new Chomp() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveEnemyMoveEffect(enemy.Id);
        var result = effect.Resolve(gameState).SingleUnresolvedState();
        var expectedEffectStack = enemy.GetMoveEffects();
        Assert.AreEqual(gameState.WithEffects(expectedEffectStack), result);
    }

    [Test]
    public void OnlyPlacesEffectsOfTargetEnemyOnEffectStack()
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), IntendedMove = new Thrash() };
        var otherEnemy = new JawWorm { Id = EnemyId.New(), IntendedMove = new Chomp() };
        var gameState = new GameState { EnemyParty = new EnemyParty(targetEnemy, otherEnemy) };
        var effect = new ResolveEnemyMoveEffect(targetEnemy.Id);
        var result = effect.Resolve(gameState).SingleUnresolvedState();
        var expectedEffectStack = targetEnemy.GetMoveEffects();
        Assert.AreEqual(gameState.WithEffects(expectedEffectStack), result);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithId()
    {
        var enemy = new JawWorm { IntendedMove = new Chomp() };
        var gameState = new GameState { EnemyParty = new EnemyParty(enemy) };
        var effect = new ResolveEnemyMoveEffect(EnemyId.New());
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }
}