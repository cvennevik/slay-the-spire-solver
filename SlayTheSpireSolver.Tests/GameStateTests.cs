using NUnit.Framework;
using SlayTheSpireSolver.Cards.Defend;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Enemies;
using SlayTheSpireSolver.Enemies.JawWorms;
using System;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class GameStateTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            PlayerHealth = new Health(70),
            Energy = new Energy(3),
            EnemyParty = new EnemyParty(new JawWorm { Health = new Health(40), IntendedMove = new Chomp() }),
            Hand = new Hand(new StrikeCard()),
            Turn = new Turn(1)
        };
    }

    [Test]
    public void TestEquality()
    {
        var gameState1 = CreateBasicGameState();
        var gameState2 = CreateBasicGameState();
        Assert.AreEqual(gameState1, gameState2);
    }

    [Test]
    public void TestBasicGameState()
    {
        var gameState = CreateBasicGameState();
        AssertLegalActions(gameState, new StrikeAction(gameState), new EndTurnAction(gameState));
        Assert.False(gameState.IsCombatOver());
    }

    [Test]
    public void TestEmptyHand()
    {
        var gameState = CreateBasicGameState() with { Hand = new Hand() };
        AssertLegalActions(gameState, new EndTurnAction(gameState));
        Assert.False(gameState.IsCombatOver());
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-999)]
    public void TestHealthBelowOne(int amountOfHealth)
    {
        var gameState = CreateBasicGameState() with { PlayerHealth = new Health(amountOfHealth) };
        AssertNoLegalActions(gameState);
        Assert.True(gameState.IsCombatOver());
    }

    [Test]
    public void TestNoEnemiesLeft()
    {
        var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
        AssertNoLegalActions(gameState);
        Assert.True(gameState.IsCombatOver());
    }

    [Test]
    public void TestHealthBelowOneWithNoEnemies()
    {
        var gameState = CreateBasicGameState() with
        {
            PlayerHealth = new Health(0),
            EnemyParty = new EnemyParty()
        };
        AssertNoLegalActions(gameState);
        Assert.True(gameState.IsCombatOver());
    }

    [Test]
    public void TestMoveCardFromHandToDiscardPile1()
    {
        var gameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard(), new StrikeCard()),
            DiscardPile = new DiscardPile()
        };
        var newGameState = gameState.MoveCardFromHandToDiscardPile(new StrikeCard());
        var expectedGameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard()),
            DiscardPile = new DiscardPile(new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void TestMoveCardFromHandToDiscardPile2()
    {
        var gameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard(), new StrikeCard()),
            DiscardPile = new DiscardPile(new StrikeCard())
        };
        var newGameState = gameState.MoveCardFromHandToDiscardPile(new StrikeCard());
        var expectedGameState = CreateBasicGameState() with
        {
            Hand = new Hand(new StrikeCard()),
            DiscardPile = new DiscardPile(new StrikeCard(), new StrikeCard())
        };
        Assert.AreEqual(expectedGameState, newGameState);
    }

    [Test]
    public void TestMoveCardFromHandToDiscardPile3()
    {
        var gameState = CreateBasicGameState() with { Hand = new Hand(new StrikeCard()) };
        Assert.Throws<ArgumentException>(() => gameState.MoveCardFromHandToDiscardPile(new DefendCard()));
    }

    private static void AssertLegalActions(GameState gameState, params IAction[] expectedActions)
    {
        Assert.That(gameState.GetLegalActions(), Is.EquivalentTo(expectedActions));
    }

    private static void AssertNoLegalActions(GameState gameState)
    {
        Assert.IsEmpty(gameState.GetLegalActions());
    }
}
