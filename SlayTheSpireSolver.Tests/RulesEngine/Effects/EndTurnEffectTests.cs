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
        var newGameStateWithEffectStack = effect.Apply(gameState).Single();
        var expectedEffectStack = new EffectStack(
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new DrawCardEffect(),
            new RecoverBaseEnergyEffect(),
            new IncrementTurnEffect(),
            new ResolveAllEnemyMovesEffect(),
            new ClearAllEnemyArmorEffect(),
            new MoveHandToDiscardPileEffect());
        var expectedResult = new UnresolvedGameState(gameState, expectedEffectStack);
        Assert.AreEqual(expectedResult, newGameStateWithEffectStack);
    }

    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new EndTurnEffect(), new EndTurnEffect());
    }
}