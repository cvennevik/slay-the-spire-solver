using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class ResolvableGameStateSetTests
{
    [Test]
    public void TestEmptyEquality()
    {
        Assert.AreEqual(new ResolvableGameStatePossibilitySet(), new ResolvableGameStatePossibilitySet());
    }

    [Test]
    public void TestSingleEquality()
    {
        Assert.AreEqual(new ResolvableGameStatePossibilitySet(new GameState()), new ResolvableGameStatePossibilitySet(new GameState()));
        Assert.AreEqual(new ResolvableGameStatePossibilitySet(new GameState { Turn = 3 }),
            new ResolvableGameStatePossibilitySet(new GameState { Turn = 3 }));
        Assert.AreNotEqual(new ResolvableGameStatePossibilitySet(new GameState { Turn = 2 }),
            new ResolvableGameStatePossibilitySet(new GameState { Turn = 3 }));
    }

    [Test]
    public void TestMultipleEquality()
    {
        Assert.AreEqual(new ResolvableGameStatePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new ResolvableGameStatePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
        Assert.AreEqual(new ResolvableGameStatePossibilitySet(new GameState { Turn = 2 }, new GameState { Turn = 1 }),
            new ResolvableGameStatePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
    }

    [Test]
    public void TestDuplicateEquality()
    {
        Assert.AreEqual(new ResolvableGameStatePossibilitySet(new GameState { Turn = 1 }),
            new ResolvableGameStatePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 }));
        Assert.AreEqual(new ResolvableGameStatePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new ResolvableGameStatePossibilitySet(new GameState { Turn = 1 }, new GameState { Turn = 1 },
                new GameState { Turn = 2 }));
    }
}