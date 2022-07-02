using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class ClearPlayerArmorEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState();
        var effect = new ClearPlayerArmorEffect();
        var result = effect.Resolve(gameState);
    }
}