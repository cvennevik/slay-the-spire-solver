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
        var gameState = new GameState { PlayerHealth = initialHealth, PlayerArmor = initialArmor };
        var result = damagePlayerEffect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(new GameState { PlayerHealth = expectedHealth, PlayerArmor = expectedArmor }, result);
    }


    [Test]
    [TestCase(5, 5, 0)]
    [TestCase(5, 6, -1)]
    public void KillsPlayer(int initialHealth, int damage, int expectedHealth)
    {
        var gameState = new GameState
        {
            PlayerHealth = initialHealth,
            EffectStack = new EffectStack(new NullEffect())
        };
        var damagePlayerEffect = new DamagePlayerEffect(damage);
        var result = damagePlayerEffect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = expectedHealth,
            EffectStack = new EffectStack(new NullEffect(), new EndCombatEffect())
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }
}