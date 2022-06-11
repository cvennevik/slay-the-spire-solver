using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.Tests.RulesEngine.GameStateExtensions;

[TestFixture]
public class DiscardHandTests : GameStateTests
{
    [Test]
    public void Test1()
    {
        var firstGameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DiscardPile = new DiscardPile()
        };
        var nextGameState = firstGameState.DiscardHand();
        var expectedGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Strike())
        };
        Assert.AreEqual(expectedGameState, nextGameState);
    }

    [Test]
    public void Test2()
    {
        var firstGameState = new GameState
        {
            Hand = new Hand(new Strike()),
            DiscardPile = new DiscardPile(new Defend())
        };
        var nextGameState = firstGameState.DiscardHand();
        var expectedGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Defend(), new Strike())
        };
        Assert.AreEqual(expectedGameState, nextGameState);
    }

    [Test]
    public void Test3()
    {
        var firstGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Defend())
        };
        var nextGameState = firstGameState.DiscardHand();
        Assert.AreEqual(firstGameState, nextGameState);
    }

    [Test]
    public void Test4()
    {
        var firstGameState = new GameState
        {
            Hand = new Hand(),
            DiscardPile = new DiscardPile()
        };
        var nextGameState = firstGameState.DiscardHand();
        Assert.AreEqual(firstGameState, nextGameState);
    }
}
