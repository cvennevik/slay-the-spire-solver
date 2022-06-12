using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.Tests.RulesEngine.Cards;

[TestFixture]
public class StrikeTests : CommonCardTests
{
    public StrikeTests() : base(new Strike()) { }

    [Test]
    public void TestEffect()
    {
        Assert.AreEqual(new DamageEnemyEffect(BasicGameState.EnemyParty.First(), new Damage(6)),
            Card.GetEffect(BasicGameState));
    }

    [Test]
    public void TestActions()
    {
        Assert.AreEqual(new PlayCardAction(BasicGameState, Card), Card.GetLegalActions(BasicGameState).Single());
    }
}