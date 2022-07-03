using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record TargetedCard : Card
{
    public abstract EffectStack GetTargetedEffects(EnemyId target);

    public override IReadOnlyCollection<PlayerAction> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? gameState.EnemyParty.Select(enemy => GetTargetedAction(gameState, enemy.Id)).ToArray()
            : Array.Empty<PlayerAction>();
    }

    private PlayCardAction GetTargetedAction(GameState gameState, EnemyId target)
    {
        return new PlayTargetedCardAction(gameState, this, target);
    }
}

internal abstract class TargetedCardTests<TCard> : CardTests<TCard> where TCard : TargetedCard, new()
{
    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var target = BasicGameState.EnemyParty.First().Id;
        var expectedAction = new PlayTargetedCardAction(BasicGameState, Card, target);
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }

    [Test]
    public void OneLegalActionPerEnemy()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var enemy3 = new JawWorm { Id = EnemyId.New() };
        var gameState = BasicGameState with { EnemyParty = new[] { enemy1, enemy2, enemy3 } };

        var expectedAction1 = new PlayTargetedCardAction(gameState, Card, enemy1.Id);
        var expectedAction2 = new PlayTargetedCardAction(gameState, Card, enemy2.Id);
        var expectedAction3 = new PlayTargetedCardAction(gameState, Card, enemy3.Id);
        var expectedActions = new[] { expectedAction1, expectedAction2, expectedAction3 };
        Assert.That(Card.GetLegalActions(gameState), Is.EquivalentTo(expectedActions));
    }
}