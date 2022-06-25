namespace SlayTheSpireSolver.RulesEngine.Effects;

public record MoveHandToDiscardPileEffect : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var newDiscardPile = gameState.DiscardPile;
        foreach (var card in gameState.Hand.Cards)
        {
            newDiscardPile = newDiscardPile.Add(card);
        }

        var result = gameState with { Hand = new Hand(), DiscardPile = newDiscardPile };
        return new[] { result.WithEffectStack() };
    }
}