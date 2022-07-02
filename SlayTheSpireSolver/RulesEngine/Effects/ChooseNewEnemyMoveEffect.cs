using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ChooseNewEnemyMoveEffect(EnemyId Target) : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;

        gameState = gameState.ModifyEnemy(Target, enemy =>
            enemy with { PreviousMoves = enemy.PreviousMoves.Concat(new[] { enemy.IntendedMove }).ToArray() });
        var possibleMoves = gameState.EnemyParty.Get(Target).GetNextPossibleMoves();
        var result = possibleMoves.Select(moveAndProbability =>
            gameState.ModifyEnemy(Target, enemy =>
                    enemy with { IntendedMove = moveAndProbability.Item1 })
                .WithProbability(moveAndProbability.Item2)
        ).ToArray();

        return result;
    }
}