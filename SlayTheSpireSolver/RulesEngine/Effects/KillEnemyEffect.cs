using SlayTheSpireSolver.RulesEngine.Enemies;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public class KillEnemyEffect : IEffect
{
    private readonly EnemyId _targetId;

    public KillEnemyEffect(EnemyId targetId)
    {
        _targetId = targetId;
    }

    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var enemies = gameState.EnemyParty.ToList();
        var targetIndex = enemies.FindIndex(x => x.Id == _targetId);
        if (targetIndex >= 0)
        {
            enemies.RemoveAt(targetIndex);
        }
        var newGameState = gameState with { EnemyParty = new EnemyParty(enemies.ToArray()) };
        return new[] { newGameState.WithEffectStack() };
    }
}