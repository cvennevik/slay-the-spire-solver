using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ClearPlayerArmorEffectTests
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

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new ClearPlayerArmorEffect(), new ClearPlayerArmorEffect());
    }
}