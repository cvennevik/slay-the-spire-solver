using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct AddCardToDiscardPileEffect : IEffect
{
    private readonly Card _cardToAdd;

    public AddCardToDiscardPileEffect(Card card)
    {
        _cardToAdd = card;
    }

    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newCardsInDiscardPile = gameState.DiscardPile.Cards.Append(_cardToAdd).ToArray();
        return gameState with { DiscardPile = new DiscardPile(newCardsInDiscardPile) };
    }
}