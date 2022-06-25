namespace SlayTheSpireSolver.RulesEngine.Effects;

public record MoveHandToDiscardPileEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var cardsInHand = gameState.Hand.Cards;
        var newDiscardPile = gameState.DiscardPile;
        return new[] { gameState.WithEffectStack() };
    }
}