using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return DrawCard(gameState).ToArray();
    }

    private static IReadOnlyList<GameState> DrawCard(GameState gameState)
    {
        if (gameState.DrawPile.Cards.Count == 0)
        {
            return gameState.DiscardPile.Cards.Any()
                ? DrawCard(ShuffleDiscardPileIntoDrawPile(gameState.ShuffleDiscardPileIntoDrawPile()))
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

    public static GameState ShuffleDiscardPileIntoDrawPile(GameState gameState)
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