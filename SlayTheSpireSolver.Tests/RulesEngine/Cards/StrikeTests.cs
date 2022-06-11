using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

[TestFixture]
public class StrikeTests : CommonCardTests<Strike>
{
    [Test]
    public void TestEffect()
    {
        Assert.AreEqual(new DamageEnemyEffect(BasicGameState.EnemyParty.First(), new Damage(6)),
            Card.GetEffect(BasicGameState));
    }
}