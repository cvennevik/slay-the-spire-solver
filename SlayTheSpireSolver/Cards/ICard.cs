namespace SlayTheSpireSolver.Cards;

public interface ICard
{
    IEnumerable<IAction> GetLegalActions(GameState gameState);
}
