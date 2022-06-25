using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;

namespace SlayTheSpireSolver.Tests.RulesEngine;

[TestFixture]
public class ResolvableGameStateSetTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new ResolvableGameStateSet(), new ResolvableGameStateSet());
        Assert.AreEqual(new ResolvableGameStateSet(new GameState()), new ResolvableGameStateSet(new GameState()));
        Assert.AreEqual(new ResolvableGameStateSet(new GameState { Turn = 3 }),
            new ResolvableGameStateSet(new GameState { Turn = 3 }));
    }
}