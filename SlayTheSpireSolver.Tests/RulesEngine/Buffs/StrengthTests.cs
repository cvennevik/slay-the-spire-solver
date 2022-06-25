using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Buffs;

namespace SlayTheSpireSolver.Tests.RulesEngine.Buffs;

[TestFixture]
public class StrengthTests
{
    [Test]
    public void Test()
    {
        Assert.AreEqual(new Strength(1), new Strength(1));
    }
}