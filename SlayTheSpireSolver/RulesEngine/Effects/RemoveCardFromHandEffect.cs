using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RemoveCardFromHandEffect(Card _cardToRemove) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var cardsInHand = gameState.Hand.Cards.ToList();
        cardsInHand.Remove(_cardToRemove);
        return gameState with { Hand = new Hand(cardsInHand.ToArray()) };
    }
}