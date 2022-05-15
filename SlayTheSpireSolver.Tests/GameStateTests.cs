using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;
using SlayTheSpireSolver.Enemies;
using SlayTheSpireSolver.Enemies.JawWorms;
using System.Linq;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class GameStateTests
{
    private static GameState CreateBasicGameState()
    {
        return new()
        {
            Player = new Player { Health = new Health(70) },
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
        var legalActions = gameState.GetLegalActions().ToList();
        Assert.AreEqual(2, legalActions.Count);
        Assert.Contains(new EndTurnAction(gameState), legalActions);
        Assert.Contains(new StrikeAction(gameState), legalActions);
        Assert.False(gameState.IsVictory());
        Assert.False(gameState.IsDefeat());
    }

    [Test]
    public void TestEmptyHand()
    {
        var gameState =  CreateBasicGameState() with { Hand = new Hand() };
        var legalActions = gameState.GetLegalActions();
        Assert.AreEqual(1, legalActions.Count);
        Assert.AreEqual(new EndTurnAction(gameState), legalActions.First());
        Assert.False(gameState.IsVictory());
        Assert.False(gameState.IsDefeat());
    }

    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-999)]
    public void TestHealthBelowOne(int healthValue)
    {
        var gameState = CreateBasicGameState() with { Player = new Player { Health = new Health(healthValue) } };
        Assert.IsEmpty(gameState.GetLegalActions());
        Assert.True(gameState.IsDefeat());
    }

    [Test]
    public void TestNoEnemiesLeft()
    {
        var gameState = CreateBasicGameState() with { EnemyParty = new EnemyParty() };
        Assert.True(gameState.IsVictory());
        Assert.IsEmpty(gameState.GetLegalActions());
    }

    [Test]
    public void TestHealthBelowOneWithNoEnemies()
    {
        var gameState = CreateBasicGameState() with
        {
            Player = new Player { Health = new Health(0) },
            EnemyParty = new EnemyParty()
        };
        Assert.False(gameState.IsVictory());
        Assert.True(gameState.IsDefeat());
    }
}
