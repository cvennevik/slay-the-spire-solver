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
    public void Test()
    {
        var gameState = new GameState { PlayerHealth = 10 };
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void Test2()
    {
        var gameState = new GameState { PlayerHealth = 10, EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
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