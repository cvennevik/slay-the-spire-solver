using NUnit.Framework;
using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Actions;

public record PlayUntargetedCardAction : PlayCardAction
{
    public PlayUntargetedCardAction(GameState gameState, UntargetedCard card)
        : base(gameState, card, card.GetEffect()) { }
}

[TestFixture]
internal class PlayUntargetedCardActionTests
{
    
}