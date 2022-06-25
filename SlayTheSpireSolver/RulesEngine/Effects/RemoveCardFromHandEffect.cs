using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct RemoveCardFromHandEffect : IEffect
{
    private readonly Card _cardToRemove;

    public RemoveCardFromHandEffect(Card cardToRemove)
    {
        _cardToRemove = cardToRemove;
    }

    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        var cardsInHand = gameState.Hand.Cards.ToList();
        cardsInHand.Remove(_cardToRemove);
        return new[] { new ResolvableGameState(gameState with { Hand = new Hand(cardsInHand.ToArray()) }) };
    }
}