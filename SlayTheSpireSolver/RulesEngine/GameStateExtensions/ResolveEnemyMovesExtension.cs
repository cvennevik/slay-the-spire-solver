namespace SlayTheSpireSolver.RulesEngine.GameStateExtensions;

public static class ResolveEnemyMovesExtension
{
    public static GameState ResolveEnemyMoves(this GameState gameState)
    {
        foreach (var enemy in gameState.EnemyParty)
        {
            gameState = enemy.GetIntendedMove().Resolve(gameState);
        }

        return gameState;
    }
}
