namespace SlayTheSpireSolver.Cards.Strike;

public record StrikeCard : ICard
{
    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        return new[] { new StrikeAction(gameState) };
    }
}
