using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public readonly record struct RemoveCardFromHandEffect : IEffect
{
    private readonly ICard _cardToRemove;

    public RemoveCardFromHandEffect(ICard cardToRemove)
    {
        _cardToRemove = cardToRemove;
    }

    public IReadOnlyList<GameState> ApplyTo(GameState gameState)
    {
        var cardsInHand = gameState.Hand.Cards.ToList();
        cardsInHand.Remove(_cardToRemove);
        return new[] { gameState with { Hand = new Hand(cardsInHand.ToArray()) } };
    }
}