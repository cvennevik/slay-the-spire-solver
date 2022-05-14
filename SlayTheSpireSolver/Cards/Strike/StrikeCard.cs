namespace SlayTheSpireSolver.Cards.Strike;

public record StrikeCard : ICard
{
    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        if (gameState.Enemy == null) return Array.Empty<IAction>();
        if (!gameState.Hand.Cards.Contains(this)) return Array.Empty<IAction>();

        return new[] { new StrikeAction(gameState) };
    }
}
