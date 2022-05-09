using NUnit.Framework;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class GameStateTests
{
    public class EqualityTests : GameStateTests
    {
        [Test]
        public void TestEquality1()
        {
            var gameState1 = new GameState();
            var gameState2 = new GameState();
            Assert.AreEqual(gameState1, gameState2);
        }
    }
}
