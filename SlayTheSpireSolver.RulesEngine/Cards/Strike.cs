﻿using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public record Strike : TargetedCard
{
    public override Energy Cost => 1;
    protected override string Name => "Strike";

    public override EffectStack GetTargetedEffects(EnemyId target)
    {
        return new AttackEnemyEffect(target, new Damage(6));
    }
}

[TestFixture]
internal class StrikeTests : TargetedCardTests<Strike>
{
    [Test]
    public void BroadTest()
    {
        var gameState = new GameState
        {
            Energy = 3,
            Hand = new Hand(new Strike()),
            EnemyParty = new[] { new JawWorm { Health = new Health(10, 10) } }
        };
        var action = gameState.Hand.Cards.First().GetLegalActions(gameState).Single();
        var result = action.Resolve().Single();
        var expectedGameState = new GameState
        {
            Energy = 2,
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Strike()),
            EnemyParty = new[] { new JawWorm { Health = new Health(4, 10) } }
        };
        Assert.AreEqual(expectedGameState.WithProbability(1), result);
    }
}