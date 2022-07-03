using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamageEnemyEffect(EnemyId TargetId, Damage Damage) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(TargetId))
        {
            return gameState;
        }

        var newGameState = gameState.ModifyEnemy(TargetId, enemy => DamageEnemy(enemy, Damage));

        if (newGameState.EnemyParty.Get(TargetId).Health.Amount <= 0)
        {
            return new[] { newGameState.WithEffects(new EffectStack(new KillEnemyEffect(TargetId))) };
        }

        return newGameState;
    }

    private static Enemy DamageEnemy(Enemy enemy, Damage damage)
    {
        if (damage < enemy.Armor)
        {
            return enemy with { Armor = enemy.Armor - damage };
        }

        var remainingDamage = damage - enemy.Armor;
        return enemy with { Armor = 0, Health = enemy.Health - remainingDamage };

    }
}


[TestFixture]
public class DamageEnemyEffectTests
{
    [Test]
    public void DoesNothingWhenTargetEnemyIsMissing()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = 10 }) };
        var effect = new DamageEnemyEffect(EnemyId.New(), 5);
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void DamagesSingleEnemy()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = 10 }) };
        var effect = new DamageEnemyEffect(EnemyId.Default, 5);
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = 5 }) };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedState());
    }

    [Test]
    public void KillsSingleEnemy()
    {
        var gameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = 10 }) };
        var effect = new DamageEnemyEffect(EnemyId.Default, 10);
        var expectedGameState = new GameState { EnemyParty = new EnemyParty(new JawWorm { Health = 0 }) };
        var expectedEffectStack = new EffectStack(new KillEnemyEffect(EnemyId.Default));
        Assert.AreEqual(expectedGameState.WithEffects(expectedEffectStack), effect.Resolve(gameState).SingleUnresolvedState());
    }

    [Test]
    public void OnlyDamagesTarget()
    {
        var (id1, id2, id3) = (EnemyId.New(), EnemyId.New(), EnemyId.New());
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = 10, Id = id1 },
                new JawWorm { Health = 10, Id = id2 },
                new JawWorm { Health = 10, Id = id3 }
            )
        };
        var effect = new DamageEnemyEffect(id2, 5);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = 10, Id = id1 },
                new JawWorm { Health = 5, Id = id2 },
                new JawWorm { Health = 10, Id = id3 }
            )
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedState());
    }

    [Test]
    public void OnlyKillsTarget()
    {
        var (id1, id2, id3) = (EnemyId.New(), EnemyId.New(), EnemyId.New());
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = 10, Id = id1 },
                new JawWorm { Health = 10, Id = id2 },
                new JawWorm { Health = 10, Id = id3 }
            )
        };
        var effect = new DamageEnemyEffect(id2, 10);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(
                new JawWorm { Health = 10, Id = id1 },
                new JawWorm { Health = 0, Id = id2 },
                new JawWorm { Health = 10, Id = id3 }
            )
        };
        var expectedEffectStack = new EffectStack(new KillEnemyEffect(id2));
        Assert.AreEqual(expectedGameState.WithEffects(expectedEffectStack), effect.Resolve(gameState).SingleUnresolvedState());
    }

    [Test]
    public void DamagesArmor()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = 10, Armor = 10 })
        };
        var effect = new DamageEnemyEffect(EnemyId.Default, 5);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = 10, Armor = 5 })
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedState());
    }

    [Test]
    public void DamagesThroughArmor()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = 10, Armor = 10 })
        };
        var effect = new DamageEnemyEffect(EnemyId.Default, 15);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = 5 })
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).SingleResolvedState());
    }
}