using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Relics;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndCombatEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        if (gameState.Relics.Contains(new BurningBlood()) && gameState.PlayerAlive)
            return gameState with
            {
                CombatHasEnded = true,
                PlayerHealth = Math.Min(gameState.PlayerHealth.Current + 6, gameState.PlayerMaxHealth.Current)
            };

        return gameState with { CombatHasEnded = true };
    }
}

[TestFixture]
internal class EndCombatEffectTests
{
    [Test]
    public void SetsCombatHasEndedToTrue()
    {
        var gameState = new GameState { CombatHasEnded = false };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState { CombatHasEnded = true };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }

    [Test]
    public void HealsPlayerWhenPlayerAliveWithBurningBlood()
    {
        var gameState = new GameState
        {
            PlayerHealth = 1,
            PlayerMaxHealth = 10,
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = false
        };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = 7,
            PlayerMaxHealth = 10,
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = true
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }

    [Test]
    public void HealsPlayerUpToMaxHealthWhenPlayerAliveWithBurningBlood()
    {
        var gameState = new GameState
        {
            PlayerHealth = 1,
            PlayerMaxHealth = 5,
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = false
        };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = 5,
            PlayerMaxHealth = 5,
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = true
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }

    [Test]
    public void DoesNotHealPlayerWhenPlayerDeadWithBurningBlood()
    {
        var gameState = new GameState
        {
            PlayerHealth = 0,
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = false
        };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = 0,
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = true
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }
}