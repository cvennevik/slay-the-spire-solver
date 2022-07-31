using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearPlayerArmorEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState with {PlayerArmor = 0};
    }
}

[TestFixture]
internal class ClearPlayerArmorEffectTests
{
    [Test]
    public void DoesNothingWhenNoPlayerArmor()
    {
        var gameState = new GameState {PlayerArmor = 0};
        var effect = new ClearPlayerArmorEffect();
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ClearsPlayerArmor()
    {
        var gameState = new GameState {PlayerArmor = 5};
        var effect = new ClearPlayerArmorEffect();
        var result = effect.Resolve(gameState).Single().GameState;
        Assert.AreEqual(gameState with {PlayerArmor = 0}, result);
    }
}