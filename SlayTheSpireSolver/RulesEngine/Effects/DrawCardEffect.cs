using SlayTheSpireSolver.RulesEngine.GameStateExtensions;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : IEffect
{
    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return DrawCard(gameState).ToArray();
    }

    public static IReadOnlyList<GameState> DrawCard(GameState gameState)
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