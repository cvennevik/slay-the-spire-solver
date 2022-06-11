using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class DamageEnemyEffectTests
{
    [Test]
    public void DamagesSingleEnemy()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10) }) };
        var effect = new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(5));
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = new Health(5) }) };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void KillsSingleEnemy()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10) }) };
        var effect = new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(10));
        var expectedGameState = new GameState { EnemyParty = new EnemyParty() };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void OnlyDamagesTarget()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10) },
                new JawWorm { Health = new Health(9) },
                new JawWorm { Health = new Health(8) }
            )
        };
        var effect = new DamageEnemyEffect(gameState.EnemyParty.GetEnemy(2), new Damage(5));
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10) },
                new JawWorm { Health = new Health(4) },
                new JawWorm { Health = new Health(8) }
            )
        };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void OnlyKillsTarget()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10) },
                new JawWorm { Health = new Health(9) },
                new JawWorm { Health = new Health(8) }
            )
        };
        var effect = new DamageEnemyEffect(gameState.EnemyParty.GetEnemy(2), new Damage(10));
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10) },
                new JawWorm { Health = new Health(8) }
            )
        };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void DamagesArmor()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10), Armor = new Armor(10) })
        };
        var effect = new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(5));
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10), Armor = new Armor(5) })
        };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void DamagesThroughArmor()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10), Armor = new Armor(10) })
        };
        var effect = new DamageEnemyEffect(gameState.EnemyParty.First(), new Damage(15));
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(5) })
        };
        Assert.AreEqual(expectedGameState, effect.ApplyTo(gameState).Single());
    }

    [Test]
    public void TestEquality()
    {
        var target1 = new JawWorm { Health = new Health(10) };
        var target2 = new JawWorm { Health = new Health(5) };
        Assert.AreEqual(new DamageEnemyEffect(target1, new Damage(10)), new DamageEnemyEffect(target1, new Damage(10)));
        Assert.AreNotEqual(new DamageEnemyEffect(target1, new Damage(10)), new DamageEnemyEffect(target2, new Damage(10)));
        Assert.AreNotEqual(new DamageEnemyEffect(target1, new Damage(10)), new DamageEnemyEffect(target1, new Damage(5)));
    }
}