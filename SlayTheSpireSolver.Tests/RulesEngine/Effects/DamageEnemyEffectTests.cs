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
        var effect = new DamageEnemyEffect(EnemyId.Default, 5);
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = new Health(5) }) };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void KillsSingleEnemy()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10) }) };
        var effect = new DamageEnemyEffect(EnemyId.Default, 10);
        var expectedGameState = new GameState { EnemyParty = new EnemyParty() };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void OnlyDamagesTarget()
    {
        var (id1, id2, id3) = (EnemyId.New(), EnemyId.New(), EnemyId.New());
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10), Id = id1 },
                new JawWorm { Health = new Health(10), Id = id2 },
                new JawWorm { Health = new Health(10), Id = id3 }
            )
        };
        var effect = new DamageEnemyEffect(id2, 5);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10), Id = id1 },
                new JawWorm { Health = new Health(5), Id = id2 },
                new JawWorm { Health = new Health(10), Id = id3 }
            )
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void OnlyKillsTarget()
    {
        var (id1, id2, id3) = (EnemyId.New(), EnemyId.New(), EnemyId.New());
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10), Id = id1 },
                new JawWorm { Health = new Health(10), Id = id2 },
                new JawWorm { Health = new Health(10), Id = id3 }
            )
        };
        var effect = new DamageEnemyEffect(id2, 10);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = new Health(10), Id = id1 },
                new JawWorm { Health = new Health(10), Id = id3 }
            )
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void DamagesArmor()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10), Armor = new Armor(10) })
        };
        var effect = new DamageEnemyEffect(EnemyId.Default, 5);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10), Armor = new Armor(5) })
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void DamagesThroughArmor()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10), Armor = new Armor(10) })
        };
        var effect = new DamageEnemyEffect(EnemyId.Default, 15);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(5) })
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedGameState());
    }

    [Test]
    public void TestEquality()
    {
        var id = EnemyId.New();
        Assert.AreEqual(new DamageEnemyEffect(id, 10), new DamageEnemyEffect(id, 10));
        Assert.AreNotEqual(new DamageEnemyEffect(id, 10), new DamageEnemyEffect(id, 5));
        Assert.AreNotEqual(new DamageEnemyEffect(id, 10), new DamageEnemyEffect(EnemyId.New(), 10));
    }
}