namespace SlayTheSpireSolver;

public class StrikeCard : ICard
{
    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        return new[] { new StrikeAction(gameState) };
    }
}
