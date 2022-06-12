using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

[TestFixture]
public class DefendTests : CommonCardTests<Defend>
{
    [Test]
    public void TestEffect()
    {
        Assert.AreEqual(new GainPlayerArmorEffect(new Armor(5)), Card.GetEffect(BasicGameState));
    }
}