using NUnit.Framework;
using SlayTheSpireSolver.Enemies;
using SlayTheSpireSolver.Enemies.JawWorms;
using System;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class EndTurnActionTests
{
    [Test]
    public void CannotEndTurnAfterWinning()
    {
        var gameState = new GameState();
        Assert.Throws<ArgumentException>(() => new EndTurnAction(gameState));
    }

    [Test]
    public void CannotEndTurnWhenDefeated()
    {
        var gameState = new GameState
        {
            Player = new Player { Health = new Health(0) },
            EnemyParty = new EnemyParty(new JawWorm())
        };
        Assert.Throws<ArgumentException>(() => new EndTurnAction(gameState));
    }

    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void TestEndTurn(int initialTurnNumber, int expectedTurnNumber)
    {
        var gameState = CreateBasicGameState() with { Turn = new Turn(initialTurnNumber) };
        var endTurnAction = new EndTurnAction(gameState);
        var newGameState = endTurnAction.Resolve();
        Assert.AreEqual(new Turn(expectedTurnNumber), newGameState.Turn);
        Assert.AreEqual(new Health(38), newGameState.Player.Health);
    }

    [Test]
    public void TestEquality1()
    {
        var gameState = CreateBasicGameState();
        var action1 = new EndTurnAction(gameState);
        var action2 = new EndTurnAction(gameState);
        Assert.AreEqual(action1, action2);
    }

    [Test]
    public void TestEquality2()
    {
        var action1 = new EndTurnAction(CreateBasicGameState());
        var action2 = new EndTurnAction(CreateBasicGameState());
        Assert.AreEqual(action1, action2);
    }

    [Test]
    public void TestEquality3()
    {
        var action1 = new EndTurnAction(CreateBasicGameState());
        var action2 = new EndTurnAction(CreateBasicGameState() with { Turn = new Turn(2) });
        Assert.AreNotEqual(action1, action2);
    }

    private static GameState CreateBasicGameState()
    {
        return new GameState
        {
            EnemyParty = new EnemyParty(new JawWorm { IntendedMove = new Chomp() }),
            Player = new Player { Health = new Health(50) }
        };
    }
}
