using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AttackEnemyEffect(EnemyId Target, Damage Damage) : Effect
{
    public virtual PossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;
        var damage = gameState.EnemyParty.Get(Target).Vulnerable.Any() ? Damage.AgainstVulnerableEnemy() : Damage;
        return gameState.WithAddedEffects(new DamageEnemyEffect(Target, damage));
    }
}

[TestFixture]
internal class AttackEnemyEffectTests
{
    [Test]
    public void DoesNothingWhenNoEnemies()
    {
        var gameState = new GameState { Turn = 3 };
        var effect = new AttackEnemyEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(result, gameState);
    }

    [Test]
    public void DoesNothingWhenNoEnemyWithTargetId()
    {
        var gameState = new GameState { EnemyParty = new[] { new JawWorm { Id = EnemyId.New(), Health = 10 } } };
        var effect = new AttackEnemyEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(result, gameState);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void AddsDamageEnemyEffect(int damage)
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), Health = 10 };
        var otherEnemy = new JawWorm { Id = EnemyId.New(), Health = 15 };
        var gameState = new GameState { EnemyParty = new[] { targetEnemy, otherEnemy } };
        var effect = new AttackEnemyEffect(targetEnemy.Id, damage);
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState with
        {
            EffectStack = new EffectStack(new DamageEnemyEffect(targetEnemy.Id, damage))
        }, result);
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 3)]
    [TestCase(3, 4)]
    [TestCase(4, 6)]
    public void AddsDamageEnemyEffectWithExtraDamageAgainstVulnerableEnemy(int attackDamage, int dealtDamage)
    {
        var targetEnemy = new JawWorm { Id = EnemyId.New(), Health = 10, Vulnerable = 1 };
        var otherEnemy = new JawWorm { Id = EnemyId.New(), Health = 15 };
        var gameState = new GameState { EnemyParty = new[] { targetEnemy, otherEnemy } };
        var effect = new AttackEnemyEffect(targetEnemy.Id, attackDamage);
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState with
        {
            EffectStack = new EffectStack(new DamageEnemyEffect(targetEnemy.Id, dealtDamage))
        }, result);
    }
}