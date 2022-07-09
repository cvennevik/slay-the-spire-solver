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
    }
}