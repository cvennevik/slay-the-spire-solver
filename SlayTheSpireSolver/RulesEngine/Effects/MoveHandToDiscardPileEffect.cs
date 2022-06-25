namespace SlayTheSpireSolver.RulesEngine.Effects;

public record MoveHandToDiscardPileEffect : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        var newDiscardPile = gameState.Hand.Cards.Aggregate(
            gameState.DiscardPile, (current, card) => current.Add(card));

        return gameState with { Hand = new Hand(), DiscardPile = newDiscardPile };
    }
}