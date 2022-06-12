using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class MoveCardFromHandToDiscardPileExtension
{
    public static GameState MoveCardFromHandToDiscardPile(this GameState gameState, Card card)
    {
        return gameState with
        {
            Hand = gameState.Hand.Remove(card),
            DiscardPile = gameState.DiscardPile.Add(card)
        };
    }
}
