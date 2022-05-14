namespace SlayTheSpireSolver;

public interface ICard
{
    IEnumerable<IAction> GetLegalActions(GameState gameState);
}
