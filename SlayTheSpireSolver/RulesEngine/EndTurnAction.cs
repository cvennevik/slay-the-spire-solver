using SlayTheSpireSolver.RulesEngine.GameStateExtensions;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine;

public record EndTurnAction : IAction
{
    public GameState GameState { get; }

    public static bool IsLegal(GameState gameState)
    {
        return !gameState.IsCombatOver();
    }

    public EndTurnAction(GameState gameState)
    {
        if (!IsLegal(gameState)) throw new ArgumentException("Illegal EndTurn action");

        GameState = gameState;
    }

    public IReadOnlyList<GameState> ResolveToPossibleStates()
    {
        var workingGameState = GameState.ClearEnemyArmor();
        foreach (var enemy in workingGameState.EnemyParty)
        {
            workingGameState = enemy.GetIntendedMove().Resolve(workingGameState);
        }

        workingGameState = workingGameState with { Turn = new Turn(GameState.Turn.Number + 1) };
        return new[] { workingGameState };
    }
}
