using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record TargetedCard : Card
{
    public abstract Effect GetTargetedEffect(EnemyId target);

    public override IReadOnlyCollection<Action> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? gameState.EnemyParty.Select(enemy => GetTargetedAction(gameState, enemy.Id)).ToArray()
            : Array.Empty<Action>();
    }

    private Action GetTargetedAction(GameState gameState, EnemyId target)
    {
        return new Action(gameState, new EffectStack(
            new AddCardToDiscardPileEffect(this),
            GetTargetedEffect(target),
            new RemoveCardFromHandEffect(this),
            new RemoveEnergyEffect(GetCost())));
    }
}

internal class TargetedCardTests<TCard> : CardTests<TCard> where TCard : TargetedCard, new()
{
    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new Action(BasicGameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetTargetedEffect(BasicGameState.EnemyParty.First().Id),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }

    [Test]
    public void OneLegalActionPerEnemy()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var enemy3 = new JawWorm { Id = EnemyId.New() };
        var gameState = BasicGameState with { EnemyParty = new[] { enemy1, enemy2, enemy3 } };

        var expectedAction1 = new Action(gameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetTargetedEffect(enemy1.Id),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        var expectedAction2 = new Action(gameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetTargetedEffect(enemy2.Id),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        var expectedAction3 = new Action(gameState, new EffectStack(
            new AddCardToDiscardPileEffect(Card),
            Card.GetTargetedEffect(enemy3.Id),
            new RemoveCardFromHandEffect(Card),
            new RemoveEnergyEffect(Card.GetCost())));
        var expectedActions = new[] { expectedAction1, expectedAction2, expectedAction3 };
        Assert.That(Card.GetLegalActions(gameState), Is.EquivalentTo(expectedActions));
    }
}