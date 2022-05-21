namespace SlayTheSpireSolver.RulesEngine;

public interface IEnemyMove
{
    GameState Resolve(GameState gameState);
}
