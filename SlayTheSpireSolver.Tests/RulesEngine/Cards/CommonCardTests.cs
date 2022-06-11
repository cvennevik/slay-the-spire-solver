using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

[TestFixture]
public class CommonCardTests
{
    [TestFixture]
    public class CommonStrikeTests : CommonCardTestsBase<Strike>
    {
        [Test]
        public void TestEffect()
        {
            Assert.AreEqual(new DamageEnemyEffect(BasicGameState.EnemyParty.First(), new Damage(6)),
                Card.GetEffect(BasicGameState));
        }
    }

    [TestFixture]
    public class CommonDefendTests : CommonCardTestsBase<Defend>
    {
        [Test]
        public void TestEffect()
        {
            Assert.AreEqual(new GainPlayerArmorEffect(5), Card.GetEffect(BasicGameState));
        }
    }
}