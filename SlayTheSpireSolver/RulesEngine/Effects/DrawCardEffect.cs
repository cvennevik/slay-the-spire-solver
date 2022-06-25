namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        GameState gameState1 = gameState;
        IReadOnlyList<GameState> ret;
        if (!gameState1.DrawPile.Cards.Any() && gameState1.DiscardPile.Cards.Any())
        {
            gameState1 = ShuffleDiscardPileIntoDrawPile(gameState1);
        }

        if (gameState1.DrawPile.Cards.Any())
        {
            ret = gameState1.DrawPile.Cards
                .Select(card => gameState1 with
                {
                    Hand = gameState1.Hand.Add(card), DrawPile = gameState1.DrawPile.Remove(card)
                }).ToArray();
        }
        else
        {
            ret = new[] { gameState1 };
        }

        return ret.ToArray();
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