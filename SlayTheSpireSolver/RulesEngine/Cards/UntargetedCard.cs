using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public abstract record UntargetedCard : Card
{
    public abstract EffectStack GetEffects();

    public override IReadOnlyCollection<PlayCardAction> GetLegalActions(GameState gameState)
    {
        return CanBePlayed(gameState)
            ? new[] { new PlayUntargetedCardAction(gameState, this) }
            : Array.Empty<PlayCardAction>();
    }
}

internal abstract class UntargetedCardTests<TCard> : CardTests<TCard> where TCard : UntargetedCard, new()
{
    [Test]
    public void OneLegalActionForBasicGameState()
    {
        var expectedAction = new PlayUntargetedCardAction(BasicGameState, Card);
        Assert.AreEqual(expectedAction, Card.GetLegalActions(BasicGameState).Single());
    }
}