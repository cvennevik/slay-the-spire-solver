using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Buffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class AddEnemyStrengthEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var effect = new AddEnemyStrengthEffect(EnemyId.Default, 5);
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