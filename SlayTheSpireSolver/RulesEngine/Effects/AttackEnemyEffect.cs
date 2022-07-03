using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackEnemyEffect(EnemyId Target, Damage Damage) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;
        var damage = gameState.EnemyParty.Get(Target).Vulnerable.Any() ? Damage.AgainstVulnerableEnemy() : Damage;
        return gameState.WithEffects(new DamageEnemyEffect(Target, damage));
    }
}

[TestFixture]
public class AttackEnemyEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { Turn = 3 };
        var effect = new AttackEnemyEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(result, gameState);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { Id = EnemyId.New(), Health = 10 } } };
        var effect = new AttackEnemyEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(result, gameState);
    }

    [Test]
    public void AddsDamageEnemyEffect()
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), Health = 10 };
        var otherEnemy = new JawWorm { Id = EnemyId.New(), Health = 15 };
        var gameState = new GameState { EnemyParty = new[] { targetEnemy, otherEnemy } };
        var effect = new AttackEnemyEffect(targetEnemy.Id, new Damage(1));
        var result = effect.Resolve(gameState).SingleUnresolvedState();
        Assert.AreEqual(gameState.WithEffects(new DamageEnemyEffect(targetEnemy.Id, 1)), result);
    }
}