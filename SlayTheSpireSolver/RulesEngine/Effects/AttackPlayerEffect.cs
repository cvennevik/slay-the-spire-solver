using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackPlayerEffect(EnemyId EnemyId, Damage BaseDamage) : Effect
{
    public override ResolvablePossibilitySet OldResolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(EnemyId)) return gameState;
        var enemyStrength = gameState.EnemyParty.Get(EnemyId).Strength;
        var damagePlayerEffect = new DamagePlayerEffect(BaseDamage + enemyStrength);

        return gameState.WithEffects(damagePlayerEffect);
    }
}

[TestFixture]
internal class AttackPlayerEffectTests
{
    [Test]
    public void DoesNothingWhenEnemyMissing()
    {
        var gameState = new GameState { PlayerHealth = 10 };
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
        var result = effect.NewResolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void AddsDamagePlayerEffect()
    {
        var gameState = new GameState { PlayerHealth = 10, EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
        var result = effect.NewResolve(gameState).Single().GameState;
        var expectedResult = gameState.WithAddedEffects(new EffectStack(new DamagePlayerEffect(1)));
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void AddsEnemyStrengthToDamagePlayerEffect()
    {
        var gameState = new GameState { PlayerHealth = 10, EnemyParty = new EnemyParty(new JawWorm { Strength = 5}) };
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
        var result = effect.OldResolve(gameState).SingleUnresolvedState();
        var expectedResult = gameState.WithEffects(new EffectStack(new DamagePlayerEffect(6)));
        Assert.AreEqual(expectedResult, result);
    }
}