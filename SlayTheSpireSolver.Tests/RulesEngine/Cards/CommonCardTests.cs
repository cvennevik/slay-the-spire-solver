using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

[TestFixture]
public class CommonCardTests
{
    [TestFixture]
    public class CommonStrikeTests : CommonCardTestsBase<Strike> { }

    [TestFixture]
    public class CommonDefendTests : CommonCardTestsBase<Defend> { }
}