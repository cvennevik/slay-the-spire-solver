using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamageEnemyEffect(EnemyId TargetId, Damage Damage) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(TargetId)) return gameState;

        var newGameState = gameState.ModifyEnemy(TargetId, enemy => DamageEnemy(enemy, Damage));

        if (newGameState.EnemyParty.Get(TargetId).Health.Current <= 0)
            return newGameState.WithAddedEffects(new KillEnemyEffect(TargetId));

        return newGameState;
    }

    private static Enemy DamageEnemy(Enemy enemy, Damage damage)
    {
        if (damage < enemy.Armor) return enemy with { Armor = enemy.Armor - damage };

        var remainingDamage = damage - enemy.Armor;
        return enemy with { Armor = 0, Health = enemy.Health - remainingDamage };
    }
}

[TestFixture]
internal class DamageEnemyEffectTests
{
    private static EnemyParty SingleJawWormWithHealth(int current, int maximum)
    {
        return new EnemyParty(new JawWorm { Health = new Health(current, maximum) });
    }

    [Test]
    public void DoesNothingWhenTargetEnemyIsMissing()
    {
        var gameState = new GameState { EnemyParty = SingleJawWormWithHealth(10, 10) };
        var effect = new DamageEnemyEffect(EnemyId.New(), 5);
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void KillsSingleEnemy()
    {
        var effect = new DamageEnemyEffect(EnemyId.Default, 10);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(0, 10) }),
            EffectStack = new KillEnemyEffect(EnemyId.Default)
        };
        Assert.AreEqual(expectedGameState,
            effect.Resolve(new GameState { EnemyParty = SingleJawWormWithHealth(10, 10) }).Single().GameState);
    }

    [Test]
    public void OnlyDamagesTarget()
    {
        var enemy1 = new JawWorm { Health = new Health(10, 10), Id = EnemyId.New() };
        var enemy2 = new JawWorm { Health = new Health(10, 10), Id = EnemyId.New() };
        var enemy3 = new JawWorm { Health = new Health(10, 10), Id = EnemyId.New() };
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(enemy1, enemy2, enemy3)
        };
        var effect = new DamageEnemyEffect(enemy2.Id, 5);
        var damagedEnemy2 = enemy2 with { Health = new Health(5, 10) };
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(enemy1, damagedEnemy2, enemy3)
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void OnlyKillsTarget()
    {
        var enemy1 = new JawWorm { Health = new Health(10, 10), Id = EnemyId.New() };
        var enemy2 = new JawWorm { Health = new Health(10, 10), Id = EnemyId.New() };
        var enemy3 = new JawWorm { Health = new Health(10, 10), Id = EnemyId.New() };
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(enemy1, enemy2, enemy3)
        };
        var effect = new DamageEnemyEffect(enemy2.Id, 10);
        var damagedEnemy2 = enemy2 with { Health = new Health(0, 10) };
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(enemy1, damagedEnemy2, enemy3),
            EffectStack = new KillEnemyEffect(enemy2.Id)
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void DamagesArmor()
    {
        var enemy = new JawWorm { Health = new Health(10, 10), Armor = 10 };
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(enemy)
        };
        var effect = new DamageEnemyEffect(EnemyId.Default, 5);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(enemy with { Armor = 5 })
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }

    [Test]
    public void DamagesThroughArmor()
    {
        var gameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(10, 10), Armor = 10 })
        };
        var effect = new DamageEnemyEffect(EnemyId.Default, 15);
        var expectedGameState = new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(5, 10) })
        };
        Assert.AreEqual(expectedGameState, effect.Resolve(gameState).Single().GameState);
    }
}