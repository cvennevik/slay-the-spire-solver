using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Effects;
using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Enemies.JawWorms;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record TargetedCard : Card
{
    public abstract EffectStack GetTargetedEffect(EnemyId target);

    public override IReadOnlyCollection<Action> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? gameState.EnemyParty.Select(enemy => GetTargetedAction(gameState, enemy.Id)).ToArray()
            : Array.Empty<Action>();
    }

    private Action GetTargetedAction(GameState gameState, EnemyId target)
    {
        return new Action(gameState, GetEffectStackForPlayingTargetedCard(target));
    }

    private EffectStack GetEffectStackForPlayingTargetedCard(EnemyId target)
    {
        return new EffectStack(new AddCardToDiscardPileEffect(this))
            .Push(GetTargetedEffect(target))
            .Push(new RemoveCardFromHandEffect(this))
            .Push(new RemoveEnergyEffect(this.GetCost()));
    }
}

internal abstract class TargetedCardTests<TCard> : CardTests<TCard> where TCard : TargetedCard, new()
{
    private EffectStack GetExpectedEffectStack(EnemyId target)
    {
        return new EffectStack(new AddCardToDiscardPileEffect(Card))
            .Push(Card.GetTargetedEffect(target))
            .Push(new RemoveCardFromHandEffect(Card))
            .Push(new RemoveEnergyEffect(Card.GetCost()));
    }

    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedEffectStack = GetExpectedEffectStack(BasicGameState.EnemyParty.First().Id);
        var expectedAction = new Action(BasicGameState, expectedEffectStack);
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }

    [Test]
    public void OneLegalActionPerEnemy()
    {
        var enemy1 = new JawWorm { Id = EnemyId.New() };
        var enemy2 = new JawWorm { Id = EnemyId.New() };
        var enemy3 = new JawWorm { Id = EnemyId.New() };
        var gameState = BasicGameState with { EnemyParty = new[] { enemy1, enemy2, enemy3 } };

        var expectedAction1 = new Action(gameState, GetExpectedEffectStack(enemy1.Id));
        var expectedAction2 = new Action(gameState, GetExpectedEffectStack(enemy2.Id));
        var expectedAction3 = new Action(gameState, GetExpectedEffectStack(enemy3.Id));
        var expectedActions = new[] { expectedAction1, expectedAction2, expectedAction3 };
        Assert.That(Card.GetLegalActions(gameState), Is.EquivalentTo(expectedActions));
    }
}