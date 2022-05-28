namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class DrawCardExtension
{
    public static IReadOnlyList<GameState> DrawCard(this GameState gameState)
    {
        if (gameState.DrawPile.Cards.Count == 0)
        {
            return gameState.DiscardPile.Cards.Any()
                ? gameState.ShuffleDiscardPileIntoDrawPile().DrawCard()
                : new[] { gameState };
        }

        var possibleStates = new List<GameState>();
        foreach (var card in gameState.DrawPile.Cards)
        {
            possibleStates.Add(gameState with
            {
                Hand = gameState.Hand.Add(card),
                DrawPile = gameState.DrawPile.Remove(card)
            });
        }

        return possibleStates.Distinct().ToArray();
    }
}
