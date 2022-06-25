using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class PossibilitySetTests
{
    [Test]
    public void TestEmptyEquality()
    {
        Assert.AreEqual(new PossibilitySet(), new PossibilitySet());
    }

    [Test]
    public void TestSingleEquality()
    {
        Assert.AreEqual(new PossibilitySet(new GameState()), new PossibilitySet(new GameState()));
        Assert.AreEqual(new PossibilitySet(new GameState { Turn = 3 }),
            new PossibilitySet(new GameState { Turn = 3 }));
        Assert.AreNotEqual(new PossibilitySet(new GameState { Turn = 2 }),
            new PossibilitySet(new GameState { Turn = 3 }));
    }

    [Test]
    public void TestMultipleEquality()
    {
        Assert.AreEqual(new PossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new PossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
        Assert.AreEqual(new PossibilitySet(new GameState { Turn = 2 }, new GameState { Turn = 1 }),
            new PossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
    }

    [Test]
    public void TestDuplicateEquality()
    {
        Assert.AreEqual(new PossibilitySet(new GameState { Turn = 1 }),
            new PossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 }));
        Assert.AreEqual(new PossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new PossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 },
                new GameState { Turn = 2 }));
    }
}