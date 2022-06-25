using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AttackPlayerEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState();
        var effect = new AttackPlayerEffect(EnemyId.Default, new Damage(1));
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        Assert.AreEqual(gameState, result);
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