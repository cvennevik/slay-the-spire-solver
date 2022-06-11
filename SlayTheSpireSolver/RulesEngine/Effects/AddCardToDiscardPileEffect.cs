using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct AddCardToDiscardPileEffect : IEffect
{
    private readonly ICard _cardToAdd;

    public AddCardToDiscardPileEffect(ICard card)
    {
        _cardToAdd = card;
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        var newCardsInDiscardPile = gameState.DiscardPile.Cards.Append(_cardToAdd).ToArray();
        return new[] { gameState with { DiscardPile = new DiscardPile(newCardsInDiscardPile) } };
    }
}