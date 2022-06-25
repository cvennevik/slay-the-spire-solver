using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class PossibilitySetTests
{
    [Test]
    public void TestEmptyEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(), new ResolvablePossibilitySet());
    }

    [Test]
    public void TestSingleEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState()), new ResolvablePossibilitySet(new GameState()));
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 3 }),
            new ResolvablePossibilitySet(new GameState { Turn = 3 }));
        Assert.AreNotEqual(new ResolvablePossibilitySet(new GameState { Turn = 2 }),
            new ResolvablePossibilitySet(new GameState { Turn = 3 }));
    }

    [Test]
    public void TestMultipleEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 2 }, new GameState { Turn = 1 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
    }

    [Test]
    public void TestDuplicateEquality()
    {
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 1 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 }));
        Assert.AreEqual(new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new ResolvablePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 },
                new GameState { Turn = 2 }));
    }
}