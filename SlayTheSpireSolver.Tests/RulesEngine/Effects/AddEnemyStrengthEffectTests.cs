using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AddEnemyStrengthEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState();
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)),
            new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)));
        Assert.AreNotEqual(new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)),
            new AddEnemyStrengthEffect(EnemyId.Default, new Strength(2)));
        Assert.AreNotEqual(new AddEnemyStrengthEffect(EnemyId.Default, new Strength(1)),
            new AddEnemyStrengthEffect(EnemyId.New(), new Strength(1)));
    }
}