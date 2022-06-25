using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record KillEnemyEffect : IEffect
{
    private readonly EnemyId _targetId;

    public KillEnemyEffect(EnemyId targetId)
    {
        _targetId = targetId;
    }

    public IReadOnlyCollection<UnresolvedGameState> Resolve(GameState gameState)
    {
        var newGameState = gameState with { EnemyParty = gameState.EnemyParty.Remove(_targetId) };
        return new[] { newGameState.WithEffectStack() };
    }
}