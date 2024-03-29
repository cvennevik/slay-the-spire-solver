using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DamagePlayerEffect(Damage Damage) : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        if (Damage <= gameState.PlayerArmor) return gameState with { PlayerArmor = gameState.PlayerArmor - Damage };

        var remainingDamage = Damage - gameState.PlayerArmor;
        var remainingHealth = gameState.PlayerHealth - remainingDamage;
        var newGameState = gameState with { PlayerArmor = 0, PlayerHealth = remainingHealth };
        return newGameState.PlayerAlive ? newGameState : newGameState.WithAddedEffects(new EndCombatEffect());
    }
}

[TestFixture]
internal class DamagePlayerEffectTests
{
    [Test]
    [TestCase(20, 0, 0, 20, 0)]
    [TestCase(20, 0, 10, 10, 0)]
    [TestCase(20, 0, 19, 1, 0)]
    [TestCase(10, 10, 5, 10, 5)]
    [TestCase(10, 10, 10, 10, 0)]
    [TestCase(10, 10, 11, 9, 0)]
    [TestCase(10, 10, 0, 10, 10)]
    public void DamagesPlayer(int initialHealth, int initialArmor, int damage, int expectedHealth, int expectedArmor)
    {
        var damagePlayerEffect = new DamagePlayerEffect(damage);
        var gameState = new GameState
        {
            PlayerHealth = new Health(initialHealth, 100), PlayerArmor = initialArmor
        };
        var result = damagePlayerEffect.Resolve(gameState).Single().GameState;
        var expectedGameState = new GameState
        {
            PlayerHealth = new Health(expectedHealth, 100), PlayerArmor = expectedArmor
        };
        Assert.AreEqual(expectedGameState, result);
    }


    [Test]
    [TestCase(5, 5, 0)]
    [TestCase(5, 6, -1)]
    public void KillsPlayer(int initialHealth, int damage, int expectedHealth)
    {
        var gameState = new GameState
        {
            PlayerHealth = new Health(initialHealth, 100),
            EffectStack = new EffectStack(new NullEffect())
        };
        var damagePlayerEffect = new DamagePlayerEffect(damage);
        var result = damagePlayerEffect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = new Health(expectedHealth, 100),
            EffectStack = new EffectStack(new NullEffect(), new EndCombatEffect())
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }
}