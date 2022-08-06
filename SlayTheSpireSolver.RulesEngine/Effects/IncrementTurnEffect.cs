using NUnit.Framework;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record IncrementTurnEffect : Effect
{
    public override PossibilitySet Resolve(GameState gameState)
    {
        return gameState with { Turn = gameState.Turn.Number + 1 };
    }
}

[TestFixture]
internal class IncrementTurnEffectTests
{
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void Test(int initialTurn, int expectedTurn)
    {
        var gameState = new GameState { PlayerHealth = 21, Turn = initialTurn };
        var effect = new IncrementTurnEffect();
        var newGameState = effect.Resolve(gameState).Single().GameState;
        var expectedGameState = gameState with { Turn = expectedTurn };
        Assert.AreEqual(expectedGameState, newGameState);
    }
}