using NUnit.Framework;
using SlayTheSpireSolver.TestHelpers;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ClearPlayerArmorEffect : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
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
        var gameState = new GameState {PlayerArmor = 0, Turn = 3};
        var effect = new ClearPlayerArmorEffect();
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState, result);
    }

    [Test]
    public void ClearsPlayerArmor()
    {
        var gameState = new GameState {PlayerArmor = 5, Turn = 3};
        var effect = new ClearPlayerArmorEffect();
        var result = effect.Resolve(gameState).SingleResolvedState();
        Assert.AreEqual(gameState with {PlayerArmor = 0}, result);
    }
}