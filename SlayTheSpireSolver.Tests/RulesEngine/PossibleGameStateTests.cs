using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class PossibleGameStateTests
{
    [TestFixture]
    public class EqualityTests : PossibleGameStateTests
    {
        [Test]
        public void Test1()
        {
            var possibleGameState1 = new PossibleGameState(new GameState(), new Probability(1));
            var possibleGameState2 = new PossibleGameState(new GameState(), new Probability(1));
            Assert.AreEqual(possibleGameState1, possibleGameState2);
        }

        [Test]
        public void Test2()
        {
            var possibleGameState1 = new PossibleGameState(new GameState(), new Probability(1));
            var possibleGameState2 = new PossibleGameState(new GameState(), new Probability(0));
            Assert.AreNotEqual(possibleGameState1, possibleGameState2);
        }

        [Test]
        public void Test3()
        {
            var possibleGameState1 = new PossibleGameState(new GameState() with { Turn = new Turn(2) }, new Probability(1));
            var possibleGameState2 = new PossibleGameState(new GameState() with { Turn = new Turn(3) }, new Probability(1));
            Assert.AreNotEqual(possibleGameState1, possibleGameState2);
        }
    }
}
