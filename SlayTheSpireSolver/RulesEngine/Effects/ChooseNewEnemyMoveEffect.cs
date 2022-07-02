using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record ChooseNewEnemyMoveEffect(EnemyId Target) : IEffect
{
    public ResolvablePossibilitySet Resolve(GameState gameState)
    {
        if (!gameState.EnemyParty.Has(Target)) return gameState;

        var possibleMoves = gameState.EnemyParty.Get(Target).GetNextPossibleMoves();
        var result = possibleMoves.Select(moveAndProbability =>
            gameState.ModifyEnemy(Target, enemy =>
                enemy));

        return gameState;
    }
}