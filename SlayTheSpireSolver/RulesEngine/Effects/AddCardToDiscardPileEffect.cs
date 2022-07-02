using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record AddCardToDiscardPileEffect : Effect
{
    private readonly Card _cardToAdd;

    public AddCardToDiscardPileEffect(Card card)
    {
        _cardToAdd = card;
    }

    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newCardsInDiscardPile = gameState.DiscardPile.Cards.Append(_cardToAdd).ToArray();
        return gameState with { DiscardPile = new DiscardPile(newCardsInDiscardPile) };
    }
}