using System;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class PossibleStateSetTests
{
    [TestFixture]
    public class EqualityTests : PossibleStateSetTests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual(new PossibleStateSet(), new PossibleStateSet());
        }

        [Test]
        public void Test2()
        {
            var possibleState = new PossibleState(new GameState(), new Probability(1));
            Assert.AreEqual(new PossibleStateSet(possibleState), new PossibleStateSet(possibleState));
        }

        [Test]
        public void Test3()
        {
            var possibleState = new PossibleState(new GameState(), new Probability(1));
            Assert.AreNotEqual(new PossibleStateSet(), new PossibleStateSet(possibleState));
        }

        [Test]
        public void Test4()
        {
            var possibleState1 = new PossibleState(new GameState(), new Probability(1));
            var possibleState2 = new PossibleState(new GameState(), new Probability(0));
            Assert.AreNotEqual(new PossibleStateSet(possibleState1), new PossibleStateSet(possibleState2));
        }

        [Test]
        public void Test5()
        {
            var halfProbability = new PossibleState(new GameState(), new Probability(0.5));
            var fullProbability = new PossibleState(new GameState(), new Probability(1));
            var halfProbabilityTwice = new PossibleStateSet(halfProbability, halfProbability);
            Assert.AreEqual(halfProbabilityTwice, new PossibleStateSet(fullProbability));
        }
    }

    [TestFixture]
    public class LegalValueTests : PossibleStateSetTests
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(0.5, 0.6)]
        public void SumOfProbabilitiesCannotExceedOne(double firstProbability, double secondProbability)
        {
            Assert.Throws<ArgumentException>(() => new PossibleStateSet(
                new PossibleState(new GameState(), new Probability(firstProbability)),
                new PossibleState(new GameState(), new Probability(secondProbability))
            ));
        }
    }
}
