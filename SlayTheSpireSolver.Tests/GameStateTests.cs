using NUnit.Framework;
using SlayTheSpireSolver.Cards;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Enemies.JawWorms;
using System.Linq;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class GameStateTests
{
    [Test]
    public void TestEquality()
    {
        var gameState1 = new GameState();
        var gameState2 = new GameState();
        Assert.AreEqual(gameState1, gameState2);
    }

    [Test]
    public void TestGetLegalActionsWithEmptyHand()
    {
        var gameState = new GameState
        {
            Enemy = new JawWorm { IntendedMove = new Chomp() },
            Player = new Player { Health = new Health(50) }
        };
        var legalActions = gameState.GetLegalActions();
        Assert.AreEqual(1, legalActions.Count);
        Assert.AreEqual(new EndTurnAction(gameState), legalActions.First());
    }

    [Test]
    public void TestGetLegalActionsWithStrikeInHand()
    {
        var gameState = new GameState
        {
            Enemy = new JawWorm { IntendedMove = new Chomp() },
            Player = new Player { Health = new Health(50) },
            Hand = new Hand(new StrikeCard())
        };
        var legalActions = gameState.GetLegalActions().ToList();
        Assert.AreEqual(2, legalActions.Count);
        Assert.Contains(new EndTurnAction(gameState), legalActions);
        Assert.Contains(new StrikeAction(gameState), legalActions);
    }

    [Test]
    public void TestLegalActionsWithNoEnemiesAndEmptyHand()
    {
        var gameState = new GameState
        {
            Player = new Player { Health = new Health(50) }
        };
        var legalActions = gameState.GetLegalActions().ToList();
        Assert.IsEmpty(legalActions);
    }

    [Test]
    public void TestLegalActionsWithNoEnemiesAndStrikeInHand()
    {
        var gameState = new GameState
        {
            Player = new Player { Health = new Health(50) },
            Hand = new Hand(new StrikeCard())
        };
        var legalActions = gameState.GetLegalActions().ToList();
        Assert.IsEmpty(legalActions);
    }
}
