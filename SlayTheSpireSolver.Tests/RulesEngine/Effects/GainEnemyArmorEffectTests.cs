using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class GainEnemyArmorEffectTests
{
    [Test]
    public void Test()
    {
        var effect = new GainEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm()) };
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 5 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void TestWithExistingArmor()
    {
        var effect = new GainEnemyArmorEffect(EnemyId.Default, new Armor(5));
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm {Armor = 5}) };
        var result = effect.Resolve(gameState).SingleResolvedGameState();
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Armor = 10 }) };
        Assert.AreEqual(expectedGameState, result);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new GainEnemyArmorEffect(EnemyId.Default, 10), new GainEnemyArmorEffect(EnemyId.Default, 10));
        Assert.AreNotEqual(new GainEnemyArmorEffect(EnemyId.Default, 10), new GainEnemyArmorEffect(EnemyId.Default, 5));
    }
}