namespace SlayTheSpireSolver.RulesEngine.Cards;

public interface ICard
{
    IEnumerable<IAction> GetLegalActions(GameState gameState);
}
