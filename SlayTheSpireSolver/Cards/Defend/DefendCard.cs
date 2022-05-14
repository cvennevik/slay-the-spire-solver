namespace SlayTheSpireSolver.Cards.Defend;

public record DefendCard : ICard
{
    public IEnumerable<IAction> GetLegalActions(GameState gameState)
    {
        throw new NotImplementedException();
    }
}
