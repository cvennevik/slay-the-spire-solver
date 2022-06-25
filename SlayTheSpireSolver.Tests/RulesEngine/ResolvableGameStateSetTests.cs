using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class ResolvableGameStateSetTests
{
    [Test]
    public void TestEmptyEquality()
    {
        Assert.AreEqual(new ResolvableGameStateSet(), new ResolvableGameStateSet());
    }

    [Test]
    public void TestSingleEquality()
    {
        Assert.AreEqual(new ResolvableGameStateSet(new GameState()), new ResolvableGameStateSet(new GameState()));
        Assert.AreEqual(new ResolvableGameStateSet(new GameState { Turn = 3 }),
            new ResolvableGameStateSet(new GameState { Turn = 3 }));
        Assert.AreNotEqual(new ResolvableGameStateSet(new GameState { Turn = 2 }),
            new ResolvableGameStateSet(new GameState { Turn = 3 }));
    }

    [Test]
    public void TestMultipleEquality()
    {
        Assert.AreEqual(new ResolvableGameStateSet(new GameState { Turn = 1 }, new GameState { Turn = 2 }),
            new ResolvableGameStateSet(new GameState { Turn = 1 }, new GameState { Turn = 2 }));
    }
}