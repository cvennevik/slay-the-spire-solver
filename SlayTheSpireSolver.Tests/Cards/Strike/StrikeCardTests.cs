using NUnit.Framework;
using SlayTheSpireSolver.Cards.Strike;

namespace SlayTheSpireSolver.Tests.Cards.Strike;

[TestFixture]
public class StrikeCardTests
{
    [Test]
    public void TestEquality()
    {
        Assert.AreEqual(new StrikeCard(), new StrikeCard());
    }
}
