using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record DrawCardEffect : IEffect
{
    public ResolvableGameStatePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.DrawPile.Cards.Any() && gameState.DiscardPile.Cards.Any())
        {
            var newDrawPile = gameState.DiscardPile.Cards.Aggregate(gameState.DrawPile,
                (current, card) => current.Add(card));

            gameState = gameState with
            {
                DiscardPile = new DiscardPile(),
                DrawPile = newDrawPile
            };
        }

        if (gameState.DrawPile.Cards.Any())
        {
            var results = new List<ResolvableGameStatePossibility>();
            var uniqueCards = gameState.DrawPile.Cards.Distinct();
            foreach (var uniqueCard in uniqueCards)
            {
                var newGameState = gameState with
                {
                    Hand = gameState.Hand.Add(uniqueCard),
                    DrawPile = gameState.DrawPile.Remove(uniqueCard)
                };
                var fractionOfDrawPile = (double)gameState.DrawPile.Cards.Count(x => x == uniqueCard) /
                                         gameState.DrawPile.Cards.Count;
                var probability = new Probability(fractionOfDrawPile);
                results.Add(new ResolvableGameStatePossibility(newGameState, probability));
            }

            return gameState.DrawPile.Cards.Select(card => gameState with
            {
                Hand = gameState.Hand.Add(card), DrawPile = gameState.DrawPile.Remove(card)
            }).ToArray();
        }

        return gameState;
    }
}