namespace SlayTheSpireSolver.Cards.Strike;

public record StrikeCard : ICard
{
    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        return StrikeAction.IsLegal(gameState)
            ? new[] { new StrikeAction(gameState) }
            : Array.Empty<IAction>();
    }
}
