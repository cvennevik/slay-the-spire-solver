namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        return DrawCard(gameState).ToArray();
    }

    private static IReadOnlyList<GameState> DrawCard(GameState gameState)
    {
        while (true)
        {
            if (gameState.DrawPile.Cards.Count == 0)
            {
                if (gameState.DiscardPile.Cards.Any())
                {
                    gameState = ShuffleDiscardPileIntoDrawPile(gameState);
                    continue;
                }

                return new[] { gameState };
            }

            var possibleStates = new List<GameState>();
            foreach (var card in gameState.DrawPile.Cards)
            {
                possibleStates.Add(gameState with { Hand = gameState.Hand.Add(card), DrawPile = gameState.DrawPile.Remove(card) });
            }

            return possibleStates.Distinct().ToArray();
        }
    }

    public static GameState ShuffleDiscardPileIntoDrawPile(GameState gameState)
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