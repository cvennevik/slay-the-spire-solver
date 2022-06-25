namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        return DrawCard(gameState).ToArray();
    }

    private static IReadOnlyList<GameState> DrawCard(GameState gameState)
    {
        if (!gameState.DrawPile.Cards.Any() && gameState.DiscardPile.Cards.Any())
        {
            gameState = ShuffleDiscardPileIntoDrawPile(gameState);
        }

        if (gameState.DrawPile.Cards.Any())
        {
            return gameState.DrawPile.Cards
                .Select(card => gameState with
                {
                    Hand = gameState.Hand.Add(card), DrawPile = gameState.DrawPile.Remove(card)
                }).ToArray();
        }

        return new[] { gameState };
    }

    private static GameState ShuffleDiscardPileIntoDrawPile(GameState gameState)
    {
        if (gameState.DiscardPile.Cards.Count == 0) return gameState;

        var newDrawPile = gameState.DiscardPile.Cards.Aggregate(gameState.DrawPile,
            (current, card) => current.Add(card));

        return gameState with
        {
            DiscardPile = new DiscardPile(),
            DrawPile = newDrawPile
        };
    }
}