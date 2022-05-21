using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class PossibleStateTests
{
    [TestFixture]
    public class EqualityTests : PossibleStateTests
    {
        [Test]
        public void Test1()
        {
            var possibleState1 = new PossibleState(new GameState(), new Probability(1));
            var possibleState2 = new PossibleState(new GameState(), new Probability(1));
            Assert.AreEqual(possibleState1, possibleState2);
        }

        [Test]
        public void Test2()
        {
            var possibleState1 = new PossibleState(new GameState(), new Probability(1));
            var possibleState2 = new PossibleState(new GameState(), new Probability(0));
            Assert.AreNotEqual(possibleState1, possibleState2);
        }

        [Test]
        public void Test3()
        {
            var possibleState1 = new PossibleState(new GameState() with { Turn = new Turn(2) }, new Probability(1));
            var possibleState2 = new PossibleState(new GameState() with { Turn = new Turn(3) }, new Probability(1));
            Assert.AreNotEqual(possibleState1, possibleState2);
        }
    }
}
