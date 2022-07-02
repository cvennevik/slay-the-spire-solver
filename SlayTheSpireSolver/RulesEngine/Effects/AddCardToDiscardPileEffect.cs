using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddCardToDiscardPileEffect(Card _cardToAdd) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newCardsInDiscardPile = gameState.DiscardPile.Cards.Append(_cardToAdd).ToArray();
        return gameState with { DiscardPile = new DiscardPile(newCardsInDiscardPile) };
    }
}