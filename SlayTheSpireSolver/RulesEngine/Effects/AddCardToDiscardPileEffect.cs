using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddCardToDiscardPileEffect(Card CardToAdd) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newCardsInDiscardPile = gameState.DiscardPile.Cards.Append(CardToAdd).ToArray();
        return gameState with { DiscardPile = new DiscardPile(newCardsInDiscardPile) };
    }
}