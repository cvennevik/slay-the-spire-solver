namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class ShuffleDiscardPileIntoDrawPileExtension
{
    public static GameState ShuffleDiscardPileIntoDrawPile(this GameState gameState)
    {
        if (gameState.DiscardPile.Cards.Count == 0) return gameState;

        var newDrawPile = gameState.DrawPile;
        foreach (var card in gameState.DiscardPile.Cards)
        {
            newDrawPile = newDrawPile.Add(card);
        }

        return gameState with
        {
            DiscardPile = new DiscardPile(),
            DrawPile = newDrawPile
        };
    }
}
