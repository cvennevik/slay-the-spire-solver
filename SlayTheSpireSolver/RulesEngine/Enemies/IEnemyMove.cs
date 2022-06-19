namespace SlayTheSpireSolver.RulesEngine.Enemies;

public interface IEnemyMove
{
    GameState Resolve(GameState gameState);
}
