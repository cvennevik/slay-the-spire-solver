using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect : IEffect
{
    private readonly EnemyId _targetId;

    public KillEnemyEffect(EnemyId targetId)
    {
        _targetId = targetId;
    }

    public ResolvableGameStateSet Resolve(GameState gameState)
    {
        return gameState with { EnemyParty = gameState.EnemyParty.Remove(_targetId) };
    }
}