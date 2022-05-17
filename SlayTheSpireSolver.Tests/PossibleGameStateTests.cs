using NUnit.Framework;
using System;

namespace SlayTheSpireSolver.Tests;

[TestFixture]
public class PossibleGameStateTests
{
    [TestFixture]
    public class EqualityTests : PossibleGameStateTests
    {
        [Test]
        public void Test1()
        {
            var possibleGameState1 = new PossibleGameState(new GameState(), 1);
            var possibleGameState2 = new PossibleGameState(new GameState(), 1);
            Assert.AreEqual(possibleGameState1, possibleGameState2);
        }

        [Test]
        public void Test2()
        {
            var possibleGameState1 = new PossibleGameState(new GameState(), 1);
            var possibleGameState2 = new PossibleGameState(new GameState(), 0);
            Assert.AreNotEqual(possibleGameState1, possibleGameState2);
        }

        [Test]
        public void Test3()
        {
            var possibleGameState1 = new PossibleGameState(new GameState() with { Turn = new Turn(2) }, 1);
            var possibleGameState2 = new PossibleGameState(new GameState() with { Turn = new Turn(3) }, 1);
            Assert.AreNotEqual(possibleGameState1, possibleGameState2);
        }
    }

    [TestFixture]
    public class LegalProbabilityTests : PossibleGameStateTests
    {
        [Test]
        [TestCase(-999)]
        [TestCase(-1)]
        [TestCase(-0.001)]
        [TestCase(1.001)]
        [TestCase(2)]
        [TestCase(999)]
        public void ProbabilityAboveOneOrBelowZeroNotPermitted(double probability)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PossibleGameState(new GameState(), probability));
        }
    }
}
