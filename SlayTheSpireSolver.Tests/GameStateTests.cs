using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class GameStateTests
{
    public class Equality : GameStateTests
    {
        [Test]
        public void TestEquality1()
        {
            var gameState1 = new GameState();
            var gameState2 = new GameState();
            Assert.AreEqual(gameState1, gameState2);
        }
    }

    public class GetLegalActions : GameStateTests
    {
        [Test]
        public void Test1()
        {
            var gameState = new GameState();
            IReadOnlyCollection<Action> legalActions = gameState.GetLegalActions();
            Assert.AreEqual(1, legalActions.Count);
            Assert.AreEqual(new EndTurnAction(), legalActions.First());
        }
    }
}
