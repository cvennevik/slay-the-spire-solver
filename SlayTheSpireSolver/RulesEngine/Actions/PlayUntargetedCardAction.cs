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
    [Test]
    public void Test()
    {
        var action = new PlayUntargetedCardAction(new GameState(), new Defend());
        var result = action.Resolve().Single().GameState;
    }
}