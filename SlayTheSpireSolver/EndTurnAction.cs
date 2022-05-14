namespace SlayTheSpireSolver;

public record EndTurnAction : IAction
{
    public GameState GameState { get; }

    public EndTurnAction(GameState gameState)
    {
        if (gameState.EnemyParty.Count() == 0) throw new ArgumentException("Cannot end turn with no enemies");

        GameState = gameState;
    }

    public GameState Resolve()
    {
        GameState nextGameState = GameState;
        if (GameState.EnemyParty.Count() > 0)
        {
            foreach (var enemy in GameState.EnemyParty)
            {
                nextGameState = enemy.GetIntendedMove().Resolve(GameState);
            }
        }
        return nextGameState with { Turn = new Turn(GameState.Turn.Number + 1) };
    }
}
