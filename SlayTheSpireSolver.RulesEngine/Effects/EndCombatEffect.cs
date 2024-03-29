using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Relics;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndCombatEffect : Effect
{
    // TODO: Split into WinCombat and LoseCombat effect

    public override PossibilitySet Resolve(GameState gameState)
    {
        if (gameState.Relics.Contains(new BurningBlood()) && gameState.PlayerAlive)
            return gameState with
            {
                CombatHasEnded = true,
                PlayerHealth = gameState.PlayerHealth.Heal(6)
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
            PlayerHealth = new Health(1, 70),
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = false
        };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = new Health(7, 70),
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
            PlayerHealth = new Health(67, 70),
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = false
        };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = new Health(70, 70),
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
            PlayerHealth = new Health(0, 70),
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = false
        };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            PlayerHealth = new Health(0, 70),
            Relics = new RelicCollection(new BurningBlood()),
            CombatHasEnded = true
        };
        Assert.AreEqual(expectedGameState, result.Single().GameState);
    }
}