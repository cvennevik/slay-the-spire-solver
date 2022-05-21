using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class SetOfPossibleGameStatesTests
{
    [TestFixture]
    public class EqualityTests : SetOfPossibleGameStatesTests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual(new SetOfPossibleGameStates(), new SetOfPossibleGameStates());
        }

        [Test]
        public void Test2()
        {
            var possibleGameState = new PossibleGameState(new GameState(), new Probability(1));
            Assert.AreEqual(new SetOfPossibleGameStates(possibleGameState), new SetOfPossibleGameStates(possibleGameState));
        }

        [Test]
        public void Test3()
        {
            var possibleGameState = new PossibleGameState(new GameState(), new Probability(1));
            Assert.AreNotEqual(new SetOfPossibleGameStates(), new SetOfPossibleGameStates(possibleGameState));
        }

        [Test]
        public void Test4()
        {
            var possibleGameState1 = new PossibleGameState(new GameState(), new Probability(1));
            var possibleGameState2 = new PossibleGameState(new GameState(), new Probability(0));
            Assert.AreNotEqual(new SetOfPossibleGameStates(possibleGameState1), new SetOfPossibleGameStates(possibleGameState2));
        }

        [Test]
        public void Test5()
        {
            var halfProbability = new PossibleGameState(new GameState(), new Probability(0.5));
            var fullProbability = new PossibleGameState(new GameState(), new Probability(1));
            Assert.AreEqual(new SetOfPossibleGameStates(halfProbability, halfProbability), new SetOfPossibleGameStates(fullProbability));
        }
    }

    [TestFixture]
    public class LegalValueTests : SetOfPossibleGameStatesTests
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(0.5, 0.6)]
        public void SumOfProbabilitiesCannotExceedOne(double firstProbability, double secondProbability)
        {
            Assert.Throws<ArgumentException>(() => new SetOfPossibleGameStates(
                new PossibleGameState(new GameState(), new Probability(firstProbability)),
                new PossibleGameState(new GameState(), new Probability(secondProbability))
            ));
        }
    }
}
