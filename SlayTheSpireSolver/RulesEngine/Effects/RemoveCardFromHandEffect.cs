using SlayTheSpireSolver.RulesEngine.Cards;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record RemoveCardFromHandEffect(Card CardToRemove) : Effect
{
    public override ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var cardsInHand = gameState.Hand.Cards.ToList();
        cardsInHand.Remove(CardToRemove);
        return gameState with { Hand = new Hand(cardsInHand.ToArray()) };
    }
}