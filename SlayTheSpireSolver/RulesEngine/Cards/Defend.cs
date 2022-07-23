using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Defend : UntargetedCard
{
    public Energy GetCost()
    {
        return 1;
    }

    public EffectStack GetEffects()
    {
        return new EffectStack(new GainPlayerArmorEffect(5));
    }

    public override string ToString()
    {
        return "Defend";
    }
}

[TestFixture]
internal class DefendTests : UntargetedCardTests<Defend>
{
    [Test]
    public void BroadTest()
    {
        var gameState = new GameState
        {
            Energy = 3,
            Hand = new Hand(new Defend()),
            EnemyParty = new[] { new JawWorm() },
            PlayerArmor = 2
        };
        var action = gameState.Hand.Cards.First().GetLegalActions(gameState).Single();
        var result = action.Resolve().Single();
        var expectedGameState = new GameState
        {
            Energy = 2,
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Defend()),
            EnemyParty = new[] { new JawWorm() },
            PlayerArmor = 7
        };
        Assert.AreEqual(expectedGameState.WithProbability(1), result);
    }
}