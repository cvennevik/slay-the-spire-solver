using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record TargetedCard : Card
{
    public abstract EffectStack GetTargetedEffects(EnemyId target);
    public abstract Energy GetCost();

    public PlayCardAction GetTargetedAction(GameState gameState, EnemyId target)
    {
        return new PlayTargetedCardAction(gameState, this, target);
    }
}

internal abstract class TargetedCardTests<TCard> : CardTests<TCard> where TCard : TargetedCard, Card, new()
{
    [Test]
    public void OneLegalActionPerEnemy()
    {
        var enemy1 = new JawWorm { Id = new EnemyId() };
        var enemy2 = new JawWorm { Id = new EnemyId() };
        var enemy3 = new JawWorm { Id = new EnemyId() };
        var gameState = BasicGameState with { EnemyParty = new[] { enemy1, enemy2, enemy3 } };

        var expectedAction1 = new PlayTargetedCardAction(gameState, Card, enemy1.Id);
        var expectedAction2 = new PlayTargetedCardAction(gameState, Card, enemy2.Id);
        var expectedAction3 = new PlayTargetedCardAction(gameState, Card, enemy3.Id);
        var expectedActions = new[] { expectedAction1, expectedAction2, expectedAction3 };
        Assert.That(Card.GetLegalActions(gameState), Is.EquivalentTo(expectedActions));
    }
}