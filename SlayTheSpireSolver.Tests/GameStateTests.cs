using NUnit.Framework;
using SlayTheSpireSolver.JawWorms;
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
    public void TestGetLegalActions()
    {
        var gameState = new GameState
        {
            Enemy = new JawWorm { IntendedMove = new Chomp() },
            Player = new Player { Health = new Health(50) }
        };
        var legalActions = gameState.GetLegalActions();
        Assert.AreEqual(1, legalActions.Count);
        Assert.IsInstanceOf<EndTurnAction>(legalActions.First());
    }
}
