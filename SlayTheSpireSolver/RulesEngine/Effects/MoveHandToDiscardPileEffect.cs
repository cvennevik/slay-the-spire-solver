namespace SlayTheSpireSolver.RulesEngine.Effects;

public record MoveHandToDiscardPileEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var cardsInHand = gameState.Hand.Cards;
        var newDiscardPile = gameState.DiscardPile;
        foreach (var card in cardsInHand)
        {
            newDiscardPile = newDiscardPile.Add(card);
        }
        return new[] { gameState.WithEffectStack() };
    }
}