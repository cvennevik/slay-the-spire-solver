using SlayTheSpireSolver.RulesEngine.Enemies;
using SlayTheSpireSolver.RulesEngine.Values;

namespace SlayTheSpireSolver.RulesEngine.Effects;

public record GainEnemyArmorEffect(EnemyId EnemyId, Armor Armor) : IEffect
{
    public IReadOnlyCollection<GameStateWithEffectStack> Resolve(GameState gameState)
    {
        var enemy = gameState.EnemyParty.Get(EnemyId);
        return new[] { gameState.WithEffectStack() };
    }
}