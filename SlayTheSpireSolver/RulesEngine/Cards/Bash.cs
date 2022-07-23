using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Debuffs;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Bash : TargetedCard
{
    public Energy GetCost()
    {
        return 2;
    }

    public EffectStack GetTargetedEffects(EnemyId target)
    {
        return new Effect[]
        {
            new ApplyVulnerableToEnemyEffect(target, new Vulnerable(2)),
            new AttackEnemyEffect(target, new Damage(8))
        };
    }

    public override string ToString()
    {
        return "Bash";
    }
}

[TestFixture]
internal class BashTests : TargetedCardTests<Bash>
{
    [Test]
    public void BroadTest()
    {
        var gameState = new GameState
        {
            Energy = 3,
            Hand = new Hand(new Bash()),
            EnemyParty = new[] { new JawWorm { Health = 10 } }
        };
        var action = gameState.Hand.Cards.First().GetLegalActions(gameState).Single();
        var result = action.Resolve().Single();
        var expectedGameState = new GameState
        {
            Energy = 1,
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Bash()),
            EnemyParty = new[] { new JawWorm { Health = 2, Vulnerable = 2 } }
        };
        Assert.AreEqual(expectedGameState.WithProbability(1), result);
    }
}