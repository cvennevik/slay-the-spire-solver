using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Effects;

[TestFixture]
public class EndTurnEffectTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState { Turn = 2 };
        var effect = new EndTurnEffect();
        var newGameStateWithEffectStack = effect.Resolve(gameState).Single();
        var expectedResult = new GameStateWithEffectStack(gameState,
            new EffectStack(new IncrementTurnEffect()));
        Assert.AreEqual(expectedResult, newGameStateWithEffectStack);
    }
}