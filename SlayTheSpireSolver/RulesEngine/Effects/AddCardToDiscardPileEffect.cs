using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct AddCardToDiscardPileEffect : IEffect
{
    private readonly Card _cardToAdd;

    public AddCardToDiscardPileEffect(Card card)
    {
        _cardToAdd = card;
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        var newCardsInDiscardPile = gameState.DiscardPile.Cards.Append(_cardToAdd).ToArray();
        return new[] { gameState with { DiscardPile = new DiscardPile(newCardsInDiscardPile) } };
    }

    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var result = ApplyTo(gameState);
        return result.Select(x => new GameStateWithEffectStack(x)).ToList();
    }
}