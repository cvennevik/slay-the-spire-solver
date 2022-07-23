using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;
using SlayTheSpireSolver.RulesEngine.Effects;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public readonly record struct PlayUntargetedCardAction
    (GameState GameState, UntargetedCard UntargetedCard) : PlayCardAction
{
    public Card Card => UntargetedCard;

    public PossibilitySet Resolve()
    {
        var unresolvedGameState = GameState with
        {
            EffectStack = new EffectStack(new AddCardToDiscardPileEffect(UntargetedCard))
                .Push(UntargetedCard.GetEffects())
                .Push(new RemoveCardFromHandEffect(UntargetedCard))
                .Push(new RemoveEnergyEffect(UntargetedCard.GetCost()))
        };
        return unresolvedGameState.Resolve();
    }
}

[TestFixture]
internal class PlayUntargetedCardActionTests
{
    [Test]
    public void Test()
    {
        var gameState = new GameState
        {
            Energy = 3, Hand = new Hand(new Defend())
        };
        var action = new PlayUntargetedCardAction(gameState, new Defend());
        var result = action.Resolve().Single().GameState;
        var expectedGameState = new GameState
        {
            Energy = 2,
            PlayerArmor = 5,
            Hand = new Hand(),
            DiscardPile = new DiscardPile(new Defend())
        };
        Assert.AreEqual(expectedGameState, result);
    }
}