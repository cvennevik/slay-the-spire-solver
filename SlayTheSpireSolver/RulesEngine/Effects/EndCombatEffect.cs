using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Relics;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record EndCombatEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
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
            CombatHasEnded = false, Relics = new RelicCollection(new BurningBlood()), PlayerHealth = 1
        };
        var effect = new EndCombatEffect();
        var result = effect.Resolve(gameState);
        var expectedGameState = new GameState
        {
            CombatHasEnded = true, Relics = new RelicCollection(new BurningBlood()), PlayerHealth = 7
        };
    }
}