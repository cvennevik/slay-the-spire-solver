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

        var result = gameState with { Hand = new Hand(), DiscardPile = newDiscardPile };
        return new[] { gameState.WithEffectStack() };
    }
}