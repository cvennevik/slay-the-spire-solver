using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AttackPlayerEffectTests
{
    [Test]
    public void DoesNothingWhenEnemyMissing()
    {
        var gameState = new GameState { PlayerHealth = 10 };
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void AddsDamagePlayerEffect()
    {
        var gameState = new GameState { PlayerHealth = 10, EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).Single();
        var expectedResult = gameState.WithEffects(new EffectStack(new DamagePlayerEffect(1)));
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new AttackPlayerEffect(EnemyId.Default, new Damage(1)),
            new AttackPlayerEffect(EnemyId.Default, new Damage(1)));
        Assert.AreNotEqual(new AttackPlayerEffect(EnemyId.Default, new Damage(1)),
            new AttackPlayerEffect(EnemyId.Default, new Damage(2)));
        Assert.AreNotEqual(new AttackPlayerEffect(EnemyId.Default, new Damage(1)),
            new AttackPlayerEffect(EnemyId.New(), new Damage(1)));
    }
}