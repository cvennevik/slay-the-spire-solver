using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Actions;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Cards;

public interface UntargetedCard : Card
{
    public EffectStack GetEffects();
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