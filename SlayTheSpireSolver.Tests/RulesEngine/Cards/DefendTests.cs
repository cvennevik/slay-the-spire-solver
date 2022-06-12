using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

[TestFixture]
public class DefendTests : CommonCardTests
{
    public DefendTests() : base(new Defend()) { }

    [Test]
    public void TestEffect()
    {
        Assert.AreEqual(new GainPlayerArmorEffect(5), Card.GetEffect(BasicGameState));
    }
}