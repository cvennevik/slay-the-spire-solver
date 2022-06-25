namespace SlayTheSpireSolver.RulesEngine.Effects;

public record MoveHandToDiscardPileEffect : IEffect
{
    public IReadOnlyCollection<ResolvableGameState> Resolve(GameState gameState)
    {
        var newDiscardPile = gameState.Hand.Cards.Aggregate(
            gameState.DiscardPile, (current, card) => current.Add(card));

        var result = gameState with { Hand = new Hand(), DiscardPile = newDiscardPile };
        return new[] { result.WithEffectStack() };
    }
}